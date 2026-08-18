using System;
using System.Configuration.Internal;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x020006EC RID: 1772
	[Serializable]
	public class ConfigurationException : SystemException
	{
		// Token: 0x060036CE RID: 14030 RVA: 0x000E9BED File Offset: 0x000E8BED
		private void Init(string filename, int line)
		{
			base.HResult = -2146232062;
			this._filename = filename;
			this._line = line;
		}

		// Token: 0x060036CF RID: 14031 RVA: 0x000E9C08 File Offset: 0x000E8C08
		protected ConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.Init(info.GetString("filename"), info.GetInt32("line"));
		}

		// Token: 0x060036D0 RID: 14032 RVA: 0x000E9C2E File Offset: 0x000E8C2E
		[Obsolete("This class is obsolete, to create a new exception create a System.Configuration!System.Configuration.ConfigurationErrorsException")]
		public ConfigurationException() : this(null, null, null, 0)
		{
		}

		// Token: 0x060036D1 RID: 14033 RVA: 0x000E9C3A File Offset: 0x000E8C3A
		[Obsolete("This class is obsolete, to create a new exception create a System.Configuration!System.Configuration.ConfigurationErrorsException")]
		public ConfigurationException(string message) : this(message, null, null, 0)
		{
		}

		// Token: 0x060036D2 RID: 14034 RVA: 0x000E9C46 File Offset: 0x000E8C46
		[Obsolete("This class is obsolete, to create a new exception create a System.Configuration!System.Configuration.ConfigurationErrorsException")]
		public ConfigurationException(string message, Exception inner) : this(message, inner, null, 0)
		{
		}

		// Token: 0x060036D3 RID: 14035 RVA: 0x000E9C52 File Offset: 0x000E8C52
		[Obsolete("This class is obsolete, to create a new exception create a System.Configuration!System.Configuration.ConfigurationErrorsException")]
		public ConfigurationException(string message, XmlNode node) : this(message, null, ConfigurationException.GetUnsafeXmlNodeFilename(node), ConfigurationException.GetXmlNodeLineNumber(node))
		{
		}

		// Token: 0x060036D4 RID: 14036 RVA: 0x000E9C68 File Offset: 0x000E8C68
		[Obsolete("This class is obsolete, to create a new exception create a System.Configuration!System.Configuration.ConfigurationErrorsException")]
		public ConfigurationException(string message, Exception inner, XmlNode node) : this(message, inner, ConfigurationException.GetUnsafeXmlNodeFilename(node), ConfigurationException.GetXmlNodeLineNumber(node))
		{
		}

		// Token: 0x060036D5 RID: 14037 RVA: 0x000E9C7E File Offset: 0x000E8C7E
		[Obsolete("This class is obsolete, to create a new exception create a System.Configuration!System.Configuration.ConfigurationErrorsException")]
		public ConfigurationException(string message, string filename, int line) : this(message, null, filename, line)
		{
		}

		// Token: 0x060036D6 RID: 14038 RVA: 0x000E9C8A File Offset: 0x000E8C8A
		[Obsolete("This class is obsolete, to create a new exception create a System.Configuration!System.Configuration.ConfigurationErrorsException")]
		public ConfigurationException(string message, Exception inner, string filename, int line) : base(message, inner)
		{
			this.Init(filename, line);
		}

		// Token: 0x060036D7 RID: 14039 RVA: 0x000E9C9D File Offset: 0x000E8C9D
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("filename", this._filename);
			info.AddValue("line", this._line);
		}

		// Token: 0x17000CB0 RID: 3248
		// (get) Token: 0x060036D8 RID: 14040 RVA: 0x000E9CCC File Offset: 0x000E8CCC
		public override string Message
		{
			get
			{
				string filename = this.Filename;
				if (!string.IsNullOrEmpty(filename))
				{
					if (this.Line != 0)
					{
						return string.Concat(new string[]
						{
							this.BareMessage,
							" (",
							filename,
							" line ",
							this.Line.ToString(CultureInfo.InvariantCulture),
							")"
						});
					}
					return this.BareMessage + " (" + filename + ")";
				}
				else
				{
					if (this.Line != 0)
					{
						return this.BareMessage + " (line " + this.Line.ToString("G", CultureInfo.InvariantCulture) + ")";
					}
					return this.BareMessage;
				}
			}
		}

		// Token: 0x17000CB1 RID: 3249
		// (get) Token: 0x060036D9 RID: 14041 RVA: 0x000E9D8C File Offset: 0x000E8D8C
		public virtual string BareMessage
		{
			get
			{
				return base.Message;
			}
		}

		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x060036DA RID: 14042 RVA: 0x000E9D94 File Offset: 0x000E8D94
		public virtual string Filename
		{
			get
			{
				return ConfigurationException.SafeFilename(this._filename);
			}
		}

		// Token: 0x17000CB3 RID: 3251
		// (get) Token: 0x060036DB RID: 14043 RVA: 0x000E9DA1 File Offset: 0x000E8DA1
		public virtual int Line
		{
			get
			{
				return this._line;
			}
		}

		// Token: 0x060036DC RID: 14044 RVA: 0x000E9DA9 File Offset: 0x000E8DA9
		[Obsolete("This class is obsolete, use System.Configuration!System.Configuration.ConfigurationErrorsException.GetFilename instead")]
		public static string GetXmlNodeFilename(XmlNode node)
		{
			return ConfigurationException.SafeFilename(ConfigurationException.GetUnsafeXmlNodeFilename(node));
		}

		// Token: 0x060036DD RID: 14045 RVA: 0x000E9DB8 File Offset: 0x000E8DB8
		[Obsolete("This class is obsolete, use System.Configuration!System.Configuration.ConfigurationErrorsException.GetLinenumber instead")]
		public static int GetXmlNodeLineNumber(XmlNode node)
		{
			IConfigErrorInfo configErrorInfo = node as IConfigErrorInfo;
			if (configErrorInfo != null)
			{
				return configErrorInfo.LineNumber;
			}
			return 0;
		}

		// Token: 0x060036DE RID: 14046 RVA: 0x000E9DD8 File Offset: 0x000E8DD8
		[FileIOPermission(SecurityAction.Assert, AllFiles = FileIOPermissionAccess.PathDiscovery)]
		private static string FullPathWithAssert(string filename)
		{
			string result = null;
			try
			{
				result = Path.GetFullPath(filename);
			}
			catch
			{
			}
			return result;
		}

		// Token: 0x060036DF RID: 14047 RVA: 0x000E9E04 File Offset: 0x000E8E04
		internal static string SafeFilename(string filename)
		{
			if (string.IsNullOrEmpty(filename))
			{
				return filename;
			}
			if (filename.StartsWith("http:", StringComparison.OrdinalIgnoreCase))
			{
				return filename;
			}
			try
			{
				Path.GetFullPath(filename);
			}
			catch (SecurityException)
			{
				try
				{
					string path = ConfigurationException.FullPathWithAssert(filename);
					filename = Path.GetFileName(path);
				}
				catch
				{
					filename = null;
				}
			}
			catch
			{
				filename = null;
			}
			return filename;
		}

		// Token: 0x060036E0 RID: 14048 RVA: 0x000E9E80 File Offset: 0x000E8E80
		private static string GetUnsafeXmlNodeFilename(XmlNode node)
		{
			IConfigErrorInfo configErrorInfo = node as IConfigErrorInfo;
			if (configErrorInfo != null)
			{
				return configErrorInfo.Filename;
			}
			return string.Empty;
		}

		// Token: 0x040031AB RID: 12715
		private const string HTTP_PREFIX = "http:";

		// Token: 0x040031AC RID: 12716
		private string _filename;

		// Token: 0x040031AD RID: 12717
		private int _line;
	}
}
