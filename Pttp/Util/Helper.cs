using Pttp.Entity;
using Pttp.Enums;
using Pttp.Throw;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;

namespace Pttp.Util
{
    public static class Helper
    {
        public static HttpRequest ParseRequest(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new InvalidRequestException();
            }

            var req = new HttpRequest();
            var lines = Regex.Split(content, "\r\n");
            var readLine = 0;

            // Request-line
            ParseRequestLine(req, lines[readLine++]);
            
            // Request-header
            while (!string.IsNullOrEmpty(lines[readLine]))
            {
                var line = lines[readLine++];
                var header = ParseRequestHeader(line);

                req.Headers.Add(header.Key, header.Value);
            }

            // CR + LF skip
            readLine++;

            // Request-body
            var body = string.Join("\r\n", lines, readLine, lines.Length - readLine);
            req.Content = body;

            return req;
        }

        /// <summary>
        /// Http request line 정보를 파싱합니다.
        /// </summary>
        /// <param name="req"></param>
        /// <param name="line"></param>
        public static void ParseRequestLine(HttpRequest req, string line)
        {
            var tokens = line.Split(' ');

            var method = ParseRequestMethod(tokens[0]);
            req.Method = method ?? throw new NotSupportedMethodException(tokens[0]);
            req.RequestUrl = tokens[1];
            req.RequestHttpVersion = tokens[2];
        }

        /// <summary>
        /// 주어진 스트링에 대한 HttpMethod 를 반환합니다.
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static Method? ParseRequestMethod(string method)
        {
            switch (method)
            {
                case "GET":
                    return Method.GET;

                case "POST":
                    return Method.POST;

                case "PUT":
                    return Method.PUT;

                case "DELETE":
                    return Method.DELETE;

                case "OPTIONS":
                    return Method.OPTIONS;

                default:
                    return null;
            }
        }

        /// <summary>
        /// 주어진 스트링에서 key, value 의 형태로 header 를 파싱합니다.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static KeyValuePair<string, string> ParseRequestHeader(string line)
        {
            var tokens = Regex.Split(line, ": ?");

            return new KeyValuePair<string, string>(tokens[0], tokens[1]);
        }
        
        /// <summary>
        /// URL 을 입력 받아 정상적인 형태로 가공합니다. (/test/ => /test)
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetOriginUrl(string url)
        {
            var replace = Regex.Replace(url, @"\/$", "");
            return replace == null || replace == String.Empty ? "/" : replace;
        }

        /// <summary>
        /// URL 와 HTTP method 를 결합하여 스트링으로 반환합니다.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static string UrlWithMethod(string url, string method)
        {
            return $"{method.ToUpper()}:{GetOriginUrl(url)}";
        }

    }
}
