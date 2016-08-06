using System.Security.Cryptography;
using System.Net;
using System.IO;
using System.Text;


public class Utils
{
	public static string urlopen(string url) {
		WebClient client = new WebClient ();
		Stream data = client.OpenRead (url);
		StreamReader reader = new StreamReader (data);
		return reader.ReadToEnd ();
	}
	
	public static string getHash(string text) {
		MD5 md5Hash = MD5.Create ();
		byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(text));
		StringBuilder sBuilder = new StringBuilder();
		for (int i = 0; i < data.Length; i++)
			sBuilder.Append(data[i].ToString("x2"));
		return sBuilder.ToString();
	}
}
