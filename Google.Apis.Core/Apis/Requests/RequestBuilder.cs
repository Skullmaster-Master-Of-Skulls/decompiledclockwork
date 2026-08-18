using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using Google.Apis.Logging;
using Google.Apis.Util;

namespace Google.Apis.Requests
{
	// Token: 0x02000012 RID: 18
	public class RequestBuilder
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00002638 File Offset: 0x00000838
		static RequestBuilder()
		{
			UriPatcher.PatchUriQuirks();
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000026A9 File Offset: 0x000008A9
		// (set) Token: 0x06000042 RID: 66 RVA: 0x000026B1 File Offset: 0x000008B1
		private IDictionary<string, IList<string>> PathParameters { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000026BA File Offset: 0x000008BA
		// (set) Token: 0x06000044 RID: 68 RVA: 0x000026C2 File Offset: 0x000008C2
		private List<KeyValuePair<string, string>> QueryParameters { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000045 RID: 69 RVA: 0x000026CB File Offset: 0x000008CB
		// (set) Token: 0x06000046 RID: 70 RVA: 0x000026D3 File Offset: 0x000008D3
		public Uri BaseUri { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000047 RID: 71 RVA: 0x000026DC File Offset: 0x000008DC
		// (set) Token: 0x06000048 RID: 72 RVA: 0x000026E4 File Offset: 0x000008E4
		public string Path { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000049 RID: 73 RVA: 0x000026ED File Offset: 0x000008ED
		// (set) Token: 0x0600004A RID: 74 RVA: 0x000026F5 File Offset: 0x000008F5
		public string Method
		{
			get
			{
				return this.method;
			}
			set
			{
				if (!RequestBuilder.SupportedMethods.Contains(value))
				{
					throw new ArgumentOutOfRangeException("Method");
				}
				this.method = value;
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002716 File Offset: 0x00000916
		public RequestBuilder()
		{
			this.PathParameters = new Dictionary<string, IList<string>>();
			this.QueryParameters = new List<KeyValuePair<string, string>>();
			this.Method = "GET";
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002740 File Offset: 0x00000940
		public Uri BuildUri()
		{
			StringBuilder stringBuilder = this.BuildRestPath();
			if (this.QueryParameters.Count > 0)
			{
				stringBuilder.Append(stringBuilder.ToString().Contains("?") ? "&" : "?");
				stringBuilder.Append(string.Join("&", this.QueryParameters.Select(delegate(KeyValuePair<string, string> x)
				{
					if (!string.IsNullOrEmpty(x.Value))
					{
						return string.Format("{0}={1}", Uri.EscapeDataString(x.Key), Uri.EscapeDataString(x.Value));
					}
					return Uri.EscapeDataString(x.Key);
				}).ToArray<string>()));
			}
			return new Uri(this.BaseUri, stringBuilder.ToString());
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000027D8 File Offset: 0x000009D8
		private StringBuilder BuildRestPath()
		{
			if (string.IsNullOrEmpty(this.Path))
			{
				return new StringBuilder(string.Empty);
			}
			StringBuilder stringBuilder = new StringBuilder(this.Path);
			foreach (object obj in RequestBuilder.PathParametersPattern.Matches(stringBuilder.ToString()))
			{
				string text = obj.ToString();
				string text2 = text.Substring(1, text.Length - 2);
				string text3 = string.Empty;
				if ("+#./;?&|!@=".Contains(text2[0].ToString()))
				{
					text3 = text2[0].ToString();
					text2 = text2.Substring(1);
				}
				StringBuilder stringBuilder2 = new StringBuilder();
				string[] array = text2.Split(new char[]
				{
					','
				});
				int i = 0;
				while (i < array.Length)
				{
					string text4 = array[i];
					bool flag = false;
					int num = 0;
					if (text4[text4.Length - 1] == '*')
					{
						flag = true;
						text4 = text4.Substring(0, text4.Length - 1);
					}
					if (text4.Contains(":"))
					{
						if (!int.TryParse(text4.Substring(text4.IndexOf(":") + 1), out num))
						{
							throw new ArgumentException(string.Format("Can't parse number after ':' in Path \"{0}\". Parameter is \"{1}\"", this.Path, text4), this.Path);
						}
						text4 = text4.Substring(0, text4.IndexOf(":"));
					}
					string separator = text3;
					string str = text3;
					uint num2 = <PrivateImplementationDetails>.ComputeStringHash(text3);
					if (num2 <= 705468254U)
					{
						if (num2 != 588024921U)
						{
							if (num2 != 638357778U)
							{
								if (num2 != 705468254U)
								{
									goto IL_32F;
								}
								if (!(text3 == "/"))
								{
									goto IL_32F;
								}
								if (!flag)
								{
									separator = ",";
								}
							}
							else
							{
								if (!(text3 == "#"))
								{
									goto IL_32F;
								}
								str = ((i == 0) ? "#" : ",");
								separator = ",";
							}
						}
						else
						{
							if (!(text3 == "&"))
							{
								goto IL_32F;
							}
							goto IL_302;
						}
					}
					else if (num2 <= 772578730U)
					{
						if (num2 != 722245873U)
						{
							if (num2 != 772578730U)
							{
								goto IL_32F;
							}
							if (!(text3 == "+"))
							{
								goto IL_32F;
							}
							str = ((i == 0) ? "" : ",");
							separator = ",";
						}
						else
						{
							if (!(text3 == "."))
							{
								goto IL_32F;
							}
							if (!flag)
							{
								separator = ",";
							}
						}
					}
					else if (num2 != 973910158U)
					{
						if (num2 != 1041020634U)
						{
							goto IL_32F;
						}
						if (!(text3 == ";"))
						{
							goto IL_32F;
						}
						goto IL_302;
					}
					else
					{
						if (!(text3 == "?"))
						{
							goto IL_32F;
						}
						str = ((i == 0) ? "?" : "&") + text4 + "=";
						separator = ",";
						if (flag)
						{
							separator = "&" + text4 + "=";
						}
					}
					IL_342:
					if (this.PathParameters.ContainsKey(text4))
					{
						string text5 = string.Join(separator, this.PathParameters[text4]);
						if (num != 0 && num < text5.Length)
						{
							text5 = text5.Substring(0, num);
						}
						if (text3 != "+" && text3 != "#" && this.PathParameters[text4].Count == 1)
						{
							text5 = Uri.EscapeDataString(text5);
						}
						text5 = str + text5;
						stringBuilder2.Append(text5);
						i++;
						continue;
					}
					throw new ArgumentException(string.Format("Path \"{0}\" misses a \"{1}\" parameter", this.Path, text4), this.Path);
					IL_302:
					str = text3 + text4 + "=";
					separator = ",";
					if (flag)
					{
						separator = text3 + text4 + "=";
						goto IL_342;
					}
					goto IL_342;
					IL_32F:
					if (i > 0)
					{
						str = ",";
					}
					separator = ",";
					goto IL_342;
				}
				if (text3 == ";")
				{
					if (stringBuilder2[stringBuilder2.Length - 1] == '=')
					{
						stringBuilder2 = stringBuilder2.Remove(stringBuilder2.Length - 1, 1);
					}
					stringBuilder2 = stringBuilder2.Replace("=;", ";");
				}
				stringBuilder = stringBuilder.Replace(text, stringBuilder2.ToString());
			}
			return stringBuilder;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002C80 File Offset: 0x00000E80
		public void AddParameter(RequestParameterType type, string name, string value)
		{
			name.ThrowIfNull("name");
			if (value == null)
			{
				RequestBuilder.Logger.Warning("Add parameter should not get null values. type={0}, name={1}", new object[]
				{
					type,
					name
				});
				return;
			}
			if (type != RequestParameterType.Path)
			{
				if (type != RequestParameterType.Query)
				{
					throw new ArgumentOutOfRangeException("type");
				}
				this.QueryParameters.Add(new KeyValuePair<string, string>(name, value));
				return;
			}
			else
			{
				if (!this.PathParameters.ContainsKey(name))
				{
					this.PathParameters[name] = new List<string>
					{
						value
					};
					return;
				}
				this.PathParameters[name].Add(value);
				return;
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002D1F File Offset: 0x00000F1F
		public HttpRequestMessage CreateRequest()
		{
			return new HttpRequestMessage(new HttpMethod(this.Method), this.BuildUri());
		}

		// Token: 0x04000014 RID: 20
		private static readonly ILogger Logger = ApplicationContext.Logger.ForType<RequestBuilder>();

		// Token: 0x04000015 RID: 21
		private static Regex PathParametersPattern = new Regex("{[^{}]*}*");

		// Token: 0x04000016 RID: 22
		private static IEnumerable<string> SupportedMethods = new List<string>
		{
			"GET",
			"POST",
			"PUT",
			"DELETE",
			"PATCH"
		};

		// Token: 0x0400001B RID: 27
		private string method;

		// Token: 0x0400001C RID: 28
		private const string OPERATORS = "+#./;?&|!@=";
	}
}
