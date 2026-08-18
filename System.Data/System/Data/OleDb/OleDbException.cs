using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace System.Data.OleDb
{
	// Token: 0x02000227 RID: 551
	[Serializable]
	public sealed class OleDbException : DbException
	{
		// Token: 0x06001F8B RID: 8075 RVA: 0x0027B488 File Offset: 0x0027A888
		internal OleDbException(string message, OleDbHResult errorCode, Exception inner) : base(message, inner)
		{
			base.HResult = (int)errorCode;
			this.oledbErrors = new OleDbErrorCollection(null);
		}

		// Token: 0x06001F8C RID: 8076 RVA: 0x0027B4B8 File Offset: 0x0027A8B8
		internal OleDbException(OleDbException previous, Exception inner) : base(previous.Message, inner)
		{
			base.HResult = previous.ErrorCode;
			this.oledbErrors = previous.oledbErrors;
		}

		// Token: 0x06001F8D RID: 8077 RVA: 0x0027B4F8 File Offset: 0x0027A8F8
		private OleDbException(string message, Exception inner, string source, OleDbHResult errorCode, OleDbErrorCollection errors) : base(message, inner)
		{
			this.Source = source;
			base.HResult = (int)errorCode;
			this.oledbErrors = errors;
		}

		// Token: 0x06001F8E RID: 8078 RVA: 0x0027B528 File Offset: 0x0027A928
		private OleDbException(SerializationInfo si, StreamingContext sc) : base(si, sc)
		{
			this.oledbErrors = (OleDbErrorCollection)si.GetValue("oledbErrors", typeof(OleDbErrorCollection));
		}

		// Token: 0x06001F8F RID: 8079 RVA: 0x0027B568 File Offset: 0x0027A968
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo si, StreamingContext context)
		{
			if (si == null)
			{
				throw new ArgumentNullException("si");
			}
			si.AddValue("oledbErrors", this.oledbErrors, typeof(OleDbErrorCollection));
			base.GetObjectData(si, context);
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06001F90 RID: 8080 RVA: 0x0027B5A8 File Offset: 0x0027A9A8
		[TypeConverter(typeof(OleDbException.ErrorCodeConverter))]
		public override int ErrorCode
		{
			get
			{
				return base.ErrorCode;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06001F91 RID: 8081 RVA: 0x0027B5C8 File Offset: 0x0027A9C8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public OleDbErrorCollection Errors
		{
			get
			{
				OleDbErrorCollection oleDbErrorCollection = this.oledbErrors;
				if (oleDbErrorCollection == null)
				{
					return new OleDbErrorCollection(null);
				}
				return oleDbErrorCollection;
			}
		}

		// Token: 0x06001F92 RID: 8082 RVA: 0x0027B5E8 File Offset: 0x0027A9E8
		internal bool ShouldSerializeErrors()
		{
			OleDbErrorCollection oleDbErrorCollection = this.oledbErrors;
			return oleDbErrorCollection != null && 0 < oleDbErrorCollection.Count;
		}

		// Token: 0x06001F93 RID: 8083 RVA: 0x0027B618 File Offset: 0x0027AA18
		internal static OleDbException CreateException(UnsafeNativeMethods.IErrorInfo errorInfo, OleDbHResult errorCode, Exception inner)
		{
			OleDbErrorCollection oleDbErrorCollection = new OleDbErrorCollection(errorInfo);
			string text = null;
			string text2 = null;
			if (errorInfo != null)
			{
				OleDbHResult a = errorInfo.GetDescription(out text);
				Bid.Trace("<oledb.IErrorInfo.GetDescription|API|OS|RET> %08X{HRESULT}, Description='%ls'\n", a, text);
				a = errorInfo.GetSource(out text2);
				Bid.Trace("<oledb.IErrorInfo.GetSource|API|OS|RET> %08X{HRESULT}, Source='%ls'\n", a, text2);
			}
			int count = oleDbErrorCollection.Count;
			if (0 < oleDbErrorCollection.Count)
			{
				StringBuilder stringBuilder = new StringBuilder();
				if (text != null && text != oleDbErrorCollection[0].Message)
				{
					stringBuilder.Append(text.TrimEnd(ODB.ErrorTrimCharacters));
					if (1 < count)
					{
						stringBuilder.Append(Environment.NewLine);
					}
				}
				for (int i = 0; i < count; i++)
				{
					if (0 < i)
					{
						stringBuilder.Append(Environment.NewLine);
					}
					stringBuilder.Append(oleDbErrorCollection[i].Message.TrimEnd(ODB.ErrorTrimCharacters));
				}
				text = stringBuilder.ToString();
			}
			if (ADP.IsEmpty(text))
			{
				text = ODB.NoErrorMessage(errorCode);
			}
			return new OleDbException(text, inner, text2, errorCode, oleDbErrorCollection);
		}

		// Token: 0x06001F94 RID: 8084 RVA: 0x0027B718 File Offset: 0x0027AB18
		internal static OleDbException CombineExceptions(List<OleDbException> exceptions)
		{
			if (1 < exceptions.Count)
			{
				OleDbErrorCollection oleDbErrorCollection = new OleDbErrorCollection(null);
				StringBuilder stringBuilder = new StringBuilder();
				foreach (OleDbException ex in exceptions)
				{
					oleDbErrorCollection.AddRange(ex.Errors);
					stringBuilder.Append(ex.Message);
					stringBuilder.Append(Environment.NewLine);
				}
				return new OleDbException(stringBuilder.ToString(), null, exceptions[0].Source, (OleDbHResult)exceptions[0].ErrorCode, oleDbErrorCollection);
			}
			return exceptions[0];
		}

		// Token: 0x040012F6 RID: 4854
		private OleDbErrorCollection oledbErrors;

		// Token: 0x02000228 RID: 552
		internal sealed class ErrorCodeConverter : Int32Converter
		{
			// Token: 0x06001F96 RID: 8086 RVA: 0x0027B7F8 File Offset: 0x0027ABF8
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == null)
				{
					throw ADP.ArgumentNull("destinationType");
				}
				if (destinationType == typeof(string) && value != null && value is int)
				{
					return ODB.ELookup((OleDbHResult)value);
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}
		}
	}
}
