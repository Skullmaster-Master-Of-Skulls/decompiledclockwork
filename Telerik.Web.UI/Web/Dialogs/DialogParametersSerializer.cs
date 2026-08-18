using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;
using Telerik.Web.UI;
using Telerik.Web.UI.Common;

namespace Telerik.Web.Dialogs
{
	// Token: 0x0200103B RID: 4155
	internal class DialogParametersSerializer
	{
		// Token: 0x1700339F RID: 13215
		// (get) Token: 0x0600A38E RID: 41870 RVA: 0x00246327 File Offset: 0x00244527
		// (set) Token: 0x0600A38F RID: 41871 RVA: 0x0024632F File Offset: 0x0024452F
		private DialogParameters Parameters { get; set; }

		// Token: 0x170033A0 RID: 13216
		// (get) Token: 0x0600A390 RID: 41872 RVA: 0x00246338 File Offset: 0x00244538
		public string Result
		{
			get
			{
				if (this._result == null)
				{
					this._result = this.Serialize();
				}
				return this._result;
			}
		}

		// Token: 0x170033A1 RID: 13217
		// (get) Token: 0x0600A391 RID: 41873 RVA: 0x00246354 File Offset: 0x00244554
		private static ArgumentException UnsupportedException
		{
			get
			{
				return new ArgumentException("Unsupported DialogParameters type");
			}
		}

		// Token: 0x0600A392 RID: 41874 RVA: 0x00246360 File Offset: 0x00244560
		public DialogParametersSerializer(DialogParameters parameters)
		{
			this.Parameters = parameters;
		}

		// Token: 0x0600A393 RID: 41875 RVA: 0x00246370 File Offset: 0x00244570
		public static DialogParameters Deserialize(string serialized)
		{
			HmacEnabledCryptoService service = DialogHashService.GetService();
			string text = DialogParametersSerializer.DecodeString(service.Decrypt(serialized));
			string[] array = text.Split(new char[]
			{
				';'
			});
			DialogParameters dialogParameters = new DialogParameters();
			for (int i = 0; i < array.Length; i++)
			{
				DialogParametersSerializer.AddDeserializedItem(array[i], dialogParameters);
			}
			return dialogParameters;
		}

		// Token: 0x0600A394 RID: 41876 RVA: 0x002463CC File Offset: 0x002445CC
		private string Serialize()
		{
			HmacEnabledCryptoService service = DialogHashService.GetService();
			StringWriter stringWriter = new StringWriter();
			foreach (object obj in this.Parameters.Keys)
			{
				string text = (string)obj;
				object obj2 = this.Parameters[text];
				bool flag = obj2 is Array;
				DialogParametersSerializer.SupportedDialogParameterType parameterType = DialogParametersSerializer.GetParameterType(obj2);
				string text2 = this.SerializeValue(flag, parameterType, obj2);
				stringWriter.Write("{0},{1},{2},{3};", new object[]
				{
					text,
					flag,
					(int)parameterType,
					text2
				});
			}
			DialogParametersSerializer.RemoveLastSeparator(stringWriter);
			return service.Encrypt(DialogParametersSerializer.EncodeString(stringWriter.ToString()));
		}

		// Token: 0x0600A395 RID: 41877 RVA: 0x002464B0 File Offset: 0x002446B0
		protected string SerializeValue(bool isArray, DialogParametersSerializer.SupportedDialogParameterType type, object value)
		{
			if (isArray)
			{
				return this.SerializeArray(type, (Array)value);
			}
			switch (type)
			{
			case DialogParametersSerializer.SupportedDialogParameterType.String:
				return DialogParametersSerializer.EncodeString((string)value);
			case DialogParametersSerializer.SupportedDialogParameterType.Int:
			case DialogParametersSerializer.SupportedDialogParameterType.Bool:
				return value.ToString();
			case DialogParametersSerializer.SupportedDialogParameterType.Enum:
				return ((int)value).ToString();
			case DialogParametersSerializer.SupportedDialogParameterType.DateTime:
				return ((DateTime)value).ToString(DateTimeFormatInfo.InvariantInfo);
			default:
				throw DialogParametersSerializer.UnsupportedException;
			}
		}

		// Token: 0x0600A396 RID: 41878 RVA: 0x00246528 File Offset: 0x00244728
		private string SerializeArray(DialogParametersSerializer.SupportedDialogParameterType type, Array toSerialize)
		{
			StringWriter stringWriter = new StringWriter();
			for (int i = 0; i < toSerialize.Length; i++)
			{
				stringWriter.Write("{0},", this.SerializeValue(false, type, toSerialize.GetValue(i)));
			}
			DialogParametersSerializer.RemoveLastSeparator(stringWriter);
			return DialogParametersSerializer.EncodeString(stringWriter.ToString());
		}

		// Token: 0x0600A397 RID: 41879 RVA: 0x00246578 File Offset: 0x00244778
		private static void AddDeserializedItem(string serializedItem, DialogParameters toFill)
		{
			string[] array = serializedItem.Split(new char[]
			{
				','
			});
			toFill[array[0]] = DialogParametersSerializer.DeserializeValue(bool.Parse(array[1]), (DialogParametersSerializer.SupportedDialogParameterType)int.Parse(array[2]), array[3]);
		}

		// Token: 0x0600A398 RID: 41880 RVA: 0x002465BC File Offset: 0x002447BC
		protected static object DeserializeValue(bool isArray, DialogParametersSerializer.SupportedDialogParameterType type, string serializedValue)
		{
			if (isArray)
			{
				return DialogParametersSerializer.DeserializeArray(type, serializedValue);
			}
			switch (type)
			{
			case DialogParametersSerializer.SupportedDialogParameterType.String:
				return DialogParametersSerializer.DecodeString(serializedValue);
			case DialogParametersSerializer.SupportedDialogParameterType.Int:
			case DialogParametersSerializer.SupportedDialogParameterType.Enum:
				return int.Parse(serializedValue);
			case DialogParametersSerializer.SupportedDialogParameterType.Bool:
				return bool.Parse(serializedValue);
			case DialogParametersSerializer.SupportedDialogParameterType.DateTime:
				return DateTime.Parse(serializedValue, DateTimeFormatInfo.InvariantInfo);
			default:
				throw DialogParametersSerializer.UnsupportedException;
			}
		}

		// Token: 0x0600A399 RID: 41881 RVA: 0x00246628 File Offset: 0x00244828
		protected static Array DeserializeArray(DialogParametersSerializer.SupportedDialogParameterType type, string serializedValue)
		{
			ArrayList arrayList = new ArrayList();
			if (!string.IsNullOrEmpty(serializedValue))
			{
				foreach (string serializedValue2 in DialogParametersSerializer.DecodeString(serializedValue).Split(new char[]
				{
					','
				}))
				{
					arrayList.Add(DialogParametersSerializer.DeserializeValue(false, type, serializedValue2));
				}
			}
			Type typeFromHandle;
			switch (type)
			{
			case DialogParametersSerializer.SupportedDialogParameterType.String:
				typeFromHandle = typeof(string);
				break;
			case DialogParametersSerializer.SupportedDialogParameterType.Int:
			case DialogParametersSerializer.SupportedDialogParameterType.Enum:
				typeFromHandle = typeof(int);
				break;
			case DialogParametersSerializer.SupportedDialogParameterType.Bool:
				typeFromHandle = typeof(bool);
				break;
			case DialogParametersSerializer.SupportedDialogParameterType.DateTime:
				typeFromHandle = typeof(DateTime);
				break;
			default:
				throw DialogParametersSerializer.UnsupportedException;
			}
			return arrayList.ToArray(typeFromHandle);
		}

		// Token: 0x0600A39A RID: 41882 RVA: 0x002466E8 File Offset: 0x002448E8
		protected static void RemoveLastSeparator(StringWriter writer)
		{
			StringBuilder stringBuilder = writer.GetStringBuilder();
			if (stringBuilder.Length >= 1)
			{
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
			}
		}

		// Token: 0x0600A39B RID: 41883 RVA: 0x00246715 File Offset: 0x00244915
		private static string EncodeString(string toEncode)
		{
			return Convert.ToBase64String(Encoding.UTF8.GetBytes(toEncode));
		}

		// Token: 0x0600A39C RID: 41884 RVA: 0x00246727 File Offset: 0x00244927
		private static string DecodeString(string toDecode)
		{
			return Encoding.UTF8.GetString(Convert.FromBase64String(toDecode));
		}

		// Token: 0x0600A39D RID: 41885 RVA: 0x0024673C File Offset: 0x0024493C
		private static DialogParametersSerializer.SupportedDialogParameterType GetParameterType(object parameter)
		{
			if (parameter is string || parameter is string[])
			{
				return DialogParametersSerializer.SupportedDialogParameterType.String;
			}
			if (parameter is Enum || parameter is Enum[])
			{
				return DialogParametersSerializer.SupportedDialogParameterType.Enum;
			}
			if (parameter is int || parameter is int[])
			{
				return DialogParametersSerializer.SupportedDialogParameterType.Int;
			}
			if (parameter is bool || parameter is bool[])
			{
				return DialogParametersSerializer.SupportedDialogParameterType.Bool;
			}
			if (parameter is DateTime || parameter is DateTime[])
			{
				return DialogParametersSerializer.SupportedDialogParameterType.DateTime;
			}
			throw DialogParametersSerializer.UnsupportedException;
		}

		// Token: 0x0600A39E RID: 41886 RVA: 0x002467A8 File Offset: 0x002449A8
		public static void WriteJavascriptString(TextWriter writer, string s)
		{
			writer.Write("'");
			if (s == null)
			{
				s = string.Empty;
			}
			writer.Write(s.Replace("\\", "\\\\").Replace("'", "\\'").Replace("\n", "\\n").Replace("\r", "\\r"));
			writer.Write("'");
		}

		// Token: 0x0600A39F RID: 41887 RVA: 0x00246818 File Offset: 0x00244A18
		public static void WriteJavascriptBool(TextWriter writer, bool value)
		{
			DialogParametersSerializer.WriteJavascriptString(writer, value.ToString().ToLowerInvariant());
		}

		// Token: 0x04002D7E RID: 11646
		private string _result;

		// Token: 0x0200103C RID: 4156
		protected enum SupportedDialogParameterType
		{
			// Token: 0x04002D81 RID: 11649
			String,
			// Token: 0x04002D82 RID: 11650
			Int,
			// Token: 0x04002D83 RID: 11651
			Enum,
			// Token: 0x04002D84 RID: 11652
			Bool,
			// Token: 0x04002D85 RID: 11653
			DateTime
		}
	}
}
