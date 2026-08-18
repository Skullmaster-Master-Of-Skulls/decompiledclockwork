using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Internal;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x02000028 RID: 40
	[Serializable]
	public class ConfigurationErrorsException : ConfigurationException
	{
		// Token: 0x060001DE RID: 478 RVA: 0x0000F287 File Offset: 0x0000D487
		private void Init(string filename, int line)
		{
			base.HResult = -2146232062;
			if (line == -1)
			{
				line = 0;
			}
			this._firstFilename = filename;
			this._firstLine = line;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000F2A9 File Offset: 0x0000D4A9
		public ConfigurationErrorsException(string message, Exception inner, string filename, int line) : base(message, inner)
		{
			this.Init(filename, line);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000F2BC File Offset: 0x0000D4BC
		public ConfigurationErrorsException() : this(null, null, null, 0)
		{
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000F2C8 File Offset: 0x0000D4C8
		public ConfigurationErrorsException(string message) : this(message, null, null, 0)
		{
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000F2D4 File Offset: 0x0000D4D4
		public ConfigurationErrorsException(string message, Exception inner) : this(message, inner, null, 0)
		{
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000F2E0 File Offset: 0x0000D4E0
		public ConfigurationErrorsException(string message, string filename, int line) : this(message, null, filename, line)
		{
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000F2EC File Offset: 0x0000D4EC
		public ConfigurationErrorsException(string message, XmlNode node) : this(message, null, ConfigurationErrorsException.GetUnsafeFilename(node), ConfigurationErrorsException.GetLineNumber(node))
		{
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000F302 File Offset: 0x0000D502
		public ConfigurationErrorsException(string message, Exception inner, XmlNode node) : this(message, inner, ConfigurationErrorsException.GetUnsafeFilename(node), ConfigurationErrorsException.GetLineNumber(node))
		{
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000F318 File Offset: 0x0000D518
		public ConfigurationErrorsException(string message, XmlReader reader) : this(message, null, ConfigurationErrorsException.GetUnsafeFilename(reader), ConfigurationErrorsException.GetLineNumber(reader))
		{
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000F32E File Offset: 0x0000D52E
		public ConfigurationErrorsException(string message, Exception inner, XmlReader reader) : this(message, inner, ConfigurationErrorsException.GetUnsafeFilename(reader), ConfigurationErrorsException.GetLineNumber(reader))
		{
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000F344 File Offset: 0x0000D544
		internal ConfigurationErrorsException(string message, IConfigErrorInfo errorInfo) : this(message, null, ConfigurationErrorsException.GetUnsafeConfigErrorInfoFilename(errorInfo), ConfigurationErrorsException.GetConfigErrorInfoLineNumber(errorInfo))
		{
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000F35A File Offset: 0x0000D55A
		internal ConfigurationErrorsException(string message, Exception inner, IConfigErrorInfo errorInfo) : this(message, inner, ConfigurationErrorsException.GetUnsafeConfigErrorInfoFilename(errorInfo), ConfigurationErrorsException.GetConfigErrorInfoLineNumber(errorInfo))
		{
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000F370 File Offset: 0x0000D570
		internal ConfigurationErrorsException(ConfigurationException e) : this(ConfigurationErrorsException.GetBareMessage(e), ConfigurationErrorsException.GetInnerException(e), ConfigurationErrorsException.GetUnsafeFilename(e), ConfigurationErrorsException.GetLineNumber(e))
		{
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000F390 File Offset: 0x0000D590
		internal ConfigurationErrorsException(ICollection<ConfigurationException> coll) : this(ConfigurationErrorsException.GetFirstException(coll))
		{
			if (coll.Count > 1)
			{
				this._errors = new ConfigurationException[coll.Count];
				coll.CopyTo(this._errors, 0);
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000F3C8 File Offset: 0x0000D5C8
		internal ConfigurationErrorsException(ArrayList coll) : this((ConfigurationException)((coll.Count > 0) ? coll[0] : null))
		{
			if (coll.Count > 1)
			{
				this._errors = new ConfigurationException[coll.Count];
				coll.CopyTo(this._errors, 0);
				foreach (ConfigurationException obj in this._errors)
				{
					ConfigurationException ex = (ConfigurationException)obj;
				}
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000F43C File Offset: 0x0000D63C
		private static ConfigurationException GetFirstException(ICollection<ConfigurationException> coll)
		{
			using (IEnumerator<ConfigurationException> enumerator = coll.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					return enumerator.Current;
				}
			}
			return null;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000F484 File Offset: 0x0000D684
		private static string GetBareMessage(ConfigurationException e)
		{
			if (e != null)
			{
				return e.BareMessage;
			}
			return null;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000F491 File Offset: 0x0000D691
		private static Exception GetInnerException(ConfigurationException e)
		{
			if (e != null)
			{
				return e.InnerException;
			}
			return null;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000F49E File Offset: 0x0000D69E
		[FileIOPermission(SecurityAction.Assert, AllFiles = FileIOPermissionAccess.PathDiscovery)]
		private static string GetUnsafeFilename(ConfigurationException e)
		{
			if (e != null)
			{
				return e.Filename;
			}
			return null;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000F4AB File Offset: 0x0000D6AB
		private static int GetLineNumber(ConfigurationException e)
		{
			if (e != null)
			{
				return e.Line;
			}
			return 0;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000F4B8 File Offset: 0x0000D6B8
		protected ConfigurationErrorsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			string @string = info.GetString("firstFilename");
			int @int = info.GetInt32("firstLine");
			this.Init(@string, @int);
			int int2 = info.GetInt32("count");
			if (int2 != 0)
			{
				this._errors = new ConfigurationException[int2];
				for (int i = 0; i < int2; i++)
				{
					string str = i.ToString(CultureInfo.InvariantCulture);
					string string2 = info.GetString(str + "_errors_type");
					Type type = Type.GetType(string2, true);
					if (type != typeof(ConfigurationException) && type != typeof(ConfigurationErrorsException))
					{
						throw ExceptionUtil.UnexpectedError("ConfigurationErrorsException");
					}
					this._errors[i] = (ConfigurationException)info.GetValue(str + "_errors", type);
				}
			}
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000F5A0 File Offset: 0x0000D7A0
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			int value = 0;
			base.GetObjectData(info, context);
			info.AddValue("firstFilename", this.Filename);
			info.AddValue("firstLine", this.Line);
			if (this._errors != null && this._errors.Length > 1)
			{
				value = this._errors.Length;
				for (int i = 0; i < this._errors.Length; i++)
				{
					string str = i.ToString(CultureInfo.InvariantCulture);
					info.AddValue(str + "_errors", this._errors[i]);
					info.AddValue(str + "_errors_type", this._errors[i].GetType());
				}
			}
			info.AddValue("count", value);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000F658 File Offset: 0x0000D858
		internal void SetFileAndLine(string filename, int line)
		{
			this._firstFilename = filename;
			this._firstLine = Math.Max(line, 0);
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x0000F670 File Offset: 0x0000D870
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
							this.Line.ToString(CultureInfo.CurrentCulture),
							")"
						});
					}
					return this.BareMessage + " (" + filename + ")";
				}
				else
				{
					if (this.Line != 0)
					{
						return this.BareMessage + " (line " + this.Line.ToString("G", CultureInfo.CurrentCulture) + ")";
					}
					return this.BareMessage;
				}
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000F72E File Offset: 0x0000D92E
		public override string BareMessage
		{
			get
			{
				return base.BareMessage;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x0000F736 File Offset: 0x0000D936
		public override string Filename
		{
			get
			{
				return ConfigurationErrorsException.SafeFilename(this._firstFilename);
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x0000F743 File Offset: 0x0000D943
		public override int Line
		{
			get
			{
				return this._firstLine;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x0000F74C File Offset: 0x0000D94C
		public ICollection Errors
		{
			get
			{
				if (this._errors != null)
				{
					return this._errors;
				}
				ConfigurationErrorsException ex = new ConfigurationErrorsException(this.BareMessage, base.InnerException, this._firstFilename, this._firstLine);
				return new ConfigurationException[]
				{
					ex
				};
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001FA RID: 506 RVA: 0x0000F792 File Offset: 0x0000D992
		internal ICollection<ConfigurationException> ErrorsGeneric
		{
			get
			{
				return (ICollection<ConfigurationException>)this.Errors;
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000F79F File Offset: 0x0000D99F
		public static int GetLineNumber(XmlNode node)
		{
			return ConfigurationErrorsException.GetConfigErrorInfoLineNumber(node as IConfigErrorInfo);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000F7AC File Offset: 0x0000D9AC
		public static string GetFilename(XmlNode node)
		{
			return ConfigurationErrorsException.SafeFilename(ConfigurationErrorsException.GetUnsafeFilename(node));
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000F7B9 File Offset: 0x0000D9B9
		private static string GetUnsafeFilename(XmlNode node)
		{
			return ConfigurationErrorsException.GetUnsafeConfigErrorInfoFilename(node as IConfigErrorInfo);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000F79F File Offset: 0x0000D99F
		public static int GetLineNumber(XmlReader reader)
		{
			return ConfigurationErrorsException.GetConfigErrorInfoLineNumber(reader as IConfigErrorInfo);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000F7C6 File Offset: 0x0000D9C6
		public static string GetFilename(XmlReader reader)
		{
			return ConfigurationErrorsException.SafeFilename(ConfigurationErrorsException.GetUnsafeFilename(reader));
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000F7B9 File Offset: 0x0000D9B9
		private static string GetUnsafeFilename(XmlReader reader)
		{
			return ConfigurationErrorsException.GetUnsafeConfigErrorInfoFilename(reader as IConfigErrorInfo);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000F7D3 File Offset: 0x0000D9D3
		private static int GetConfigErrorInfoLineNumber(IConfigErrorInfo errorInfo)
		{
			if (errorInfo != null)
			{
				return errorInfo.LineNumber;
			}
			return 0;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000F7E0 File Offset: 0x0000D9E0
		private static string GetUnsafeConfigErrorInfoFilename(IConfigErrorInfo errorInfo)
		{
			if (errorInfo != null)
			{
				return errorInfo.Filename;
			}
			return null;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000F7F0 File Offset: 0x0000D9F0
		[FileIOPermission(SecurityAction.Assert, AllFiles = FileIOPermissionAccess.PathDiscovery)]
		private static string ExtractFileNameWithAssert(string filename)
		{
			string fullPath = Path.GetFullPath(filename);
			return Path.GetFileName(fullPath);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000F80C File Offset: 0x0000DA0C
		internal static string SafeFilename(string filename)
		{
			if (string.IsNullOrEmpty(filename))
			{
				return filename;
			}
			if (StringUtil.StartsWithIgnoreCase(filename, "http:"))
			{
				return filename;
			}
			try
			{
				if (!Path.IsPathRooted(filename))
				{
					return filename;
				}
			}
			catch
			{
				return null;
			}
			try
			{
				string fullPath = Path.GetFullPath(filename);
			}
			catch (SecurityException)
			{
				try
				{
					filename = ConfigurationErrorsException.ExtractFileNameWithAssert(filename);
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

		// Token: 0x06000205 RID: 517 RVA: 0x0000F8A0 File Offset: 0x0000DAA0
		internal static string AlwaysSafeFilename(string filename)
		{
			if (string.IsNullOrEmpty(filename))
			{
				return filename;
			}
			if (StringUtil.StartsWithIgnoreCase(filename, "http:"))
			{
				return filename;
			}
			try
			{
				if (!Path.IsPathRooted(filename))
				{
					return filename;
				}
			}
			catch
			{
				return null;
			}
			try
			{
				filename = ConfigurationErrorsException.ExtractFileNameWithAssert(filename);
			}
			catch
			{
				filename = null;
			}
			return filename;
		}

		// Token: 0x040001C3 RID: 451
		private const string HTTP_PREFIX = "http:";

		// Token: 0x040001C4 RID: 452
		private const string SERIALIZATION_PARAM_FILENAME = "firstFilename";

		// Token: 0x040001C5 RID: 453
		private const string SERIALIZATION_PARAM_LINE = "firstLine";

		// Token: 0x040001C6 RID: 454
		private const string SERIALIZATION_PARAM_ERROR_COUNT = "count";

		// Token: 0x040001C7 RID: 455
		private const string SERIALIZATION_PARAM_ERROR_DATA = "_errors";

		// Token: 0x040001C8 RID: 456
		private const string SERIALIZATION_PARAM_ERROR_TYPE = "_errors_type";

		// Token: 0x040001C9 RID: 457
		private string _firstFilename;

		// Token: 0x040001CA RID: 458
		private int _firstLine;

		// Token: 0x040001CB RID: 459
		private ConfigurationException[] _errors;
	}
}
