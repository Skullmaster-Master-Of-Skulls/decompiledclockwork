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
	// Token: 0x02000250 RID: 592
	[Serializable]
	public sealed class OleDbException : DbException
	{
		// Token: 0x060025A4 RID: 9636 RVA: 0x0010097C File Offset: 0x000FFD7C
		internal OleDbException(string message, OleDbHResult errorCode, Exception inner) : base(message, inner)
		{
			base.HResult = (int)errorCode;
			this.oledbErrors = new OleDbErrorCollection(null);
		}

		// Token: 0x060025A5 RID: 9637 RVA: 0x001009A4 File Offset: 0x000FFDA4
		internal OleDbException(OleDbException previous, Exception inner) : base(previous.Message, inner)
		{
			base.HResult = previous.ErrorCode;
			this.oledbErrors = previous.oledbErrors;
		}

		// Token: 0x060025A6 RID: 9638 RVA: 0x001009D8 File Offset: 0x000FFDD8
		private OleDbException(string message, Exception inner, string source, OleDbHResult errorCode, OleDbErrorCollection errors) : base(message, inner)
		{
			this.Source = source;
			base.HResult = (int)errorCode;
			this.oledbErrors = errors;
		}

		// Token: 0x060025A7 RID: 9639 RVA: 0x00100A04 File Offset: 0x000FFE04
		private OleDbException(SerializationInfo si, StreamingContext sc) : base(si, sc)
		{
			this.oledbErrors = (OleDbErrorCollection)si.GetValue("oledbErrors", typeof(OleDbErrorCollection));
		}

		// Token: 0x060025A8 RID: 9640 RVA: 0x00100A3C File Offset: 0x000FFE3C
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

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x060025A9 RID: 9641 RVA: 0x00100A7C File Offset: 0x000FFE7C
		[TypeConverter(typeof(OleDbException.ErrorCodeConverter))]
		public override int ErrorCode
		{
			get
			{
				return base.ErrorCode;
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x060025AA RID: 9642 RVA: 0x00100A90 File Offset: 0x000FFE90
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

		// Token: 0x060025AB RID: 9643 RVA: 0x00100AB0 File Offset: 0x000FFEB0
		internal bool ShouldSerializeErrors()
		{
			OleDbErrorCollection oleDbErrorCollection = this.oledbErrors;
			return oleDbErrorCollection != null && 0 < oleDbErrorCollection.Count;
		}

		// Token: 0x060025AC RID: 9644 RVA: 0x00100AD4 File Offset: 0x000FFED4
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

		// Token: 0x060025AD RID: 9645 RVA: 0x00100BD4 File Offset: 0x000FFFD4
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

		// Token: 0x0400160B RID: 5643
		private OleDbErrorCollection oledbErrors;

		// Token: 0x02000405 RID: 1029
		internal sealed class ErrorCodeConverter : Int32Converter
		{
			// Token: 0x060035DB RID: 13787 RVA: 0x00147330 File Offset: 0x00146730
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
