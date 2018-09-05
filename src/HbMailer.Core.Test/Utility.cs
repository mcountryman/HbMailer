using System;
using System.Net;

using NUnit.Framework;

namespace HbMailer {
  public class Utility {
    public static void ValidateUrl(string url, string method = "GET") {
      HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
      request.Method = method;
      HttpWebResponse response = request.GetResponse() as HttpWebResponse;

      Assert.IsTrue(
        (int)response.StatusCode >= 200 &&
        (int)response.StatusCode <= 300,
        "Url '{0}' invalid",
        url
      );
    }
  }
}
