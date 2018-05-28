﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    class RESTCaller
    {
        static HttpClient client = new HttpClient();

        public static bool TestServer(string host)
        {
            try
            {
                var t = GetResponseAsync<string>(host, "/ping").Result;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static async Task<T> GetResponseAsync<T>(string host, string apiPath, string json = null)
        {
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(host + apiPath);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Accept = "application / json";

                if (json == null)
                    httpWebRequest.Method = "GET";
                else
                {
                    httpWebRequest.Method = "POST";
                    using (var streamWriter = new StreamWriter(await httpWebRequest.GetRequestStreamAsync()))
                    {
                        streamWriter.Write(json);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }
                }

                var resp = httpWebRequest.GetResponse();

                var respJson = await (new StreamReader(resp.GetResponseStream())).ReadToEndAsync();

                var obj = JObject.Parse(respJson);

                return obj.ToObject<T>();
            }
            catch(Exception ex)
            {
                return default(T);
            }
        }
    }
}
