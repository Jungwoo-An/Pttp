using Pttp.Entity;
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

            while (!string.IsNullOrEmpty(lines[readLine]))
            {
                var line = lines[readLine++];
                var header = ParseRequestHeader(line);

                req.Headers.Add(header.Key, header.Value);
            }

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
        public static HttpMethod ParseRequestMethod(string method)
        {
            switch (method)
            {
                case "GET":
                    return HttpMethod.Get;

                case "POST":
                    return HttpMethod.Post;

                case "PUT":
                    return HttpMethod.Put;

                case "DELETE":
                    return HttpMethod.Delete;

                case "OPTIONS":
                    return HttpMethod.Options;

                default:
                    return null;
            }
        }

        public static KeyValuePair<string, string> ParseRequestHeader(string line)
        {
            var tokens = Regex.Split(line, ": ?");

            return new KeyValuePair<string, string>(tokens[0], tokens[1]);
        }
    }
}
