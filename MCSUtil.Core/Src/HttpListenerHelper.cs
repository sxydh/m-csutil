﻿using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MCSUtil.Core
{
    public class FileServer
    {
        private readonly HttpListener _listener;
        private readonly string _rootDirectory;
        private readonly string _username;
        private readonly string _password;

        public FileServer() : this(50)
        {
        }

        public FileServer(int port, string root = "ROOT") : this("localhost", port, root)
        {
        }

        public FileServer(string host, int port, string root, string username = "", string password = "")
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add($"http://{host}:{port}/");
            _rootDirectory = Path.Combine(Directory.GetCurrentDirectory(), root);
            _username = username;
            _password = password;
        }

        public Task Start()
        {
            _listener.Start();
            return Task.Run(() =>
            {
                while (_listener.IsListening)
                {
                    var context = _listener.GetContext();
                    ProcessRequest(context);
                }
            });
        }

        public void Stop()
        {
            _listener.Stop();
        }

        protected virtual void ProcessRequest(HttpListenerContext context)
        {
            if (!AuthenticateUser(context))
            {
                Process401(context);
                return;
            }

            var path = context.Request.Url.AbsolutePath.TrimStart('/');
            path = HttpUtility.UrlDecode(path);
            var targetPath = Path.Combine(_rootDirectory, path);

            if (Directory.Exists(targetPath))
            {
                ProcessDirectory(context, targetPath);
                return;
            }
            
            if (File.Exists(targetPath))
            {
                ProcessFile(context, targetPath);
                return;
            }

            Process404(context);
        }

        private bool AuthenticateUser(HttpListenerContext context)
        {
            if (string.IsNullOrEmpty(_username) && string.IsNullOrEmpty(_password))
            {
                return true;
            }

            var authHeader = context.Request.Headers["Authorization"];
            if (authHeader == null)
            {
                return false;
            }

            const string prefix = "Basic ";
            if (!authHeader.StartsWith(prefix))
            {
                return false;
            }

            var encodedCredentials = authHeader.Substring(prefix.Length).Trim();
            var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials));
            var parts = credentials.Split(':');

            return parts.Length == 2 && ValidateUser(parts[0], parts[1]);
        }

        private bool ValidateUser(string username, string password)
        {
            return username == _username && password == _password;
        }

        protected virtual void Process401(HttpListenerContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.Response.AddHeader("WWW-Authenticate", "Basic realm=\"MyRealm\"");
            using (var writer = new StreamWriter(context.Response.OutputStream))
            {
                writer.WriteLine(HttpStatusCode.Unauthorized);
            }

            context.Response.Close();
        }

        protected virtual void Process404(HttpListenerContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            using (var writer = new StreamWriter(context.Response.OutputStream))
            {
                writer.WriteLine(HttpStatusCode.NotFound);
            }

            context.Response.Close();
        }

        protected virtual void ProcessDirectory(HttpListenerContext context, string directoryPath)
        {
            try
            {
                var indexPath = Path.Combine(directoryPath, "index.html");
                if (File.Exists(indexPath))
                {
                    ProcessFile(context, indexPath);
                    return;
                }

                var subDirectories = Directory.GetDirectories(directoryPath);
                var subFiles = Directory.GetFiles(directoryPath);

                using (var writer = new StreamWriter(context.Response.OutputStream))
                {
                    writer.WriteLine("<html><head><meta charset=\"UTF-8\"></head><body><ul>");
                    foreach (var subDir in subDirectories)
                    {
                        var subDirName = Path.GetFileName(subDir);
                        writer.WriteLine($"<li><a href=\"{HttpUtility.UrlEncode(subDirName)}/\">{subDirName}/</a></li>", Encoding.UTF8);
                    }

                    foreach (var subFile in subFiles)
                    {
                        var subFileName = Path.GetFileName(subFile);
                        writer.WriteLine($"<li><a href=\"{HttpUtility.UrlEncode(subFileName)}\">{subFileName}</a></li>", Encoding.UTF8);
                    }

                    writer.WriteLine("</ul></body></html>");
                }

                context.Response.ContentType = "text/html; charset=UTF-8";
                context.Response.StatusCode = (int)HttpStatusCode.OK;
            }
            catch (Exception)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            finally
            {
                context.Response.Close();
            }
        }

        protected virtual void ProcessFile(HttpListenerContext context, string filePath)
        {
            try
            {
                context.Response.ContentType = GetContentType(filePath);
                using (var fileStream = File.OpenRead(filePath))
                {
                    fileStream.CopyTo(context.Response.OutputStream);
                }

                context.Response.StatusCode = (int)HttpStatusCode.OK;
            }
            catch (Exception)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            finally
            {
                context.Response.Close();
            }
        }

        public static string GetContentType(string path)
        {
            var extension = Path.GetExtension(path).ToLower();
            switch (extension)
            {
                case ".aac": return "audio/aac";
                case ".abw": return "application/x-abiword";
                case ".apng": return "image/apng";
                case ".arc": return "application/x-freearc";
                case ".avif": return "image/avif";
                case ".avi": return "video/x-msvideo";
                case ".azw": return "application/vnd.amazon.ebook";
                case ".bin": return "application/octet-stream";
                case ".bmp": return "image/bmp";
                case ".bz": return "application/x-bzip";
                case ".bz2": return "application/x-bzip2";
                case ".cda": return "application/x-cdf";
                case ".csh": return "application/x-csh";
                case ".css": return "text/css; charset=utf-8";
                case ".csv": return "text/csv; charset=utf-8";
                case ".doc": return "application/msword";
                case ".docx": return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                case ".eot": return "application/vnd.ms-fontobject";
                case ".epub": return "application/epub+zip";
                case ".gz": return "application/gzip";
                case ".gif": return "image/gif";
                case ".htm": return "text/html; charset=UTF-8";
                case ".html": return "text/html; charset=UTF-8";
                case ".ico": return "image/vnd.microsoft.icon";
                case ".ics": return "text/calendar";
                case ".jar": return "application/java-archive";
                case ".jpeg": return "image/jpeg";
                case ".jpg": return "image/jpeg";
                case ".js": return "text/javascript; charset=utf-8";
                case ".json": return "application/json; charset=utf-8";
                case ".jsonld": return "application/ld+json";
                case ".mid": return "audio/midi";
                case ".midi": return "audio/midi";
                case ".mjs": return "text/javascript";
                case ".mp3": return "audio/mpeg";
                case ".mp4": return "video/mp4";
                case ".mpeg": return "video/mpeg";
                case ".mpkg": return "application/vnd.apple.installer+xml";
                case ".odp": return "application/vnd.oasis.opendocument.presentation";
                case ".ods": return "application/vnd.oasis.opendocument.spreadsheet";
                case ".odt": return "application/vnd.oasis.opendocument.text";
                case ".oga": return "audio/ogg";
                case ".ogv": return "video/ogg";
                case ".ogx": return "application/ogg";
                case ".opus": return "audio/ogg";
                case ".otf": return "font/otf";
                case ".png": return "image/png";
                case ".pdf": return "application/pdf";
                case ".php": return "application/x-httpd-php";
                case ".ppt": return "application/vnd.ms-powerpoint";
                case ".pptx": return "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                case ".rar": return "application/vnd.rar";
                case ".rtf": return "application/rtf";
                case ".sh": return "application/x-sh";
                case ".svg": return "image/svg+xml";
                case ".tar": return "application/x-tar";
                case ".tif": return "image/tiff";
                case ".tiff": return "image/tiff";
                case ".ts": return "video/mp2t";
                case ".ttf": return "font/ttf";
                case ".txt": return "text/plain; charset=utf-8";
                case ".vsd": return "application/vnd.visio";
                case ".wav": return "audio/wav";
                case ".weba": return "audio/webm";
                case ".webm": return "video/webm";
                case ".webp": return "image/webp";
                case ".woff": return "font/woff";
                case ".woff2": return "font/woff2";
                case ".xhtml": return "application/xhtml+xml";
                case ".xls": return "application/vnd.ms-excel";
                case ".xlsx": return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                case ".xml": return "application/xml; charset=utf-8";
                case ".xul": return "application/vnd.mozilla.xul+xml";
                case ".zip": return "application/zip";
                case ".3gp": return "video/3gpp";
                case ".3g2": return "video/3gpp2";
                case ".7z": return "application/x-7z-compressed";
                default: return "application/octet-stream";
            }
        }
    }
}