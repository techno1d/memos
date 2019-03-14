using Java.Security;
using Javax.Net.Ssl;
using System;

namespace Memos
{
	public static class SSLHelper
	{
		public delegate void ToLogHandler(string tag, string message);

		public static ToLogHandler ToLog { get; set; }

		public static void TrustEveryone()
		{
			try
			{
				HttpsURLConnection.DefaultHostnameVerifier = new HostnameVerifier();
				SSLContext context = SSLContext.GetInstance("TLS");

				context.Init(null, new ITrustManager[] { new MyX509TrustManager() }, new SecureRandom());
				HttpsURLConnection.DefaultSSLSocketFactory = context.SocketFactory;
			}
			catch (Exception exc)
			{
				ToLog?.Invoke($"{nameof(TrustEveryone)}", $"{exc.Message}");
			}
		}

		public class MyX509TrustManager : Java.Lang.Object, IX509TrustManager
		{
			public void CheckClientTrusted(Java.Security.Cert.X509Certificate[] chain, string authType)
			{

			}

			public void CheckServerTrusted(Java.Security.Cert.X509Certificate[] chain, string authType)
			{

			}

			public Java.Security.Cert.X509Certificate[] GetAcceptedIssuers()
			{
				return new Java.Security.Cert.X509Certificate[0];
			}
		}

		public class HostnameVerifier : Java.Lang.Object, IHostnameVerifier
		{
			public bool Verify(string hostname, ISSLSession session)
			{
				return true;
			}
		}
	}
}
