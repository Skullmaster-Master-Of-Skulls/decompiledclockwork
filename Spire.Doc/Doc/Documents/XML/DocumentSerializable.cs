using System;
using Spire.CompoundFile.Doc;
using Spire.Doc.Interface;

namespace Spire.Doc.Documents.XML
{
	// Token: 0x0200008F RID: 143
	public abstract class DocumentSerializable : OwnerHolder, IDocumentSerializable
	{
		// Token: 0x0600008C RID: 140 RVA: 0x00009DCC File Offset: 0x00008DCC
		protected DocumentSerializable(Document doc, DocumentObject entity) : base(doc, entity)
		{
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00009DE4 File Offset: 0x00008DE4
		void IDocumentSerializable.WriteXmlAttributes(IXDLSAttributeWriter writer)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.WriteXmlAttributes(writer);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00009E28 File Offset: 0x00008E28
		void IDocumentSerializable.WriteXmlContent(IXDLSContentWriter writer)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.XDLSHolder.WriteHolder(writer);
			this.WriteXmlContent(writer);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00009E78 File Offset: 0x00008E78
		void IDocumentSerializable.ReadXmlAttributes(IXDLSAttributeReader reader)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			this.ReadXmlAttributes(reader);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00009EBC File Offset: 0x00008EBC
		bool IDocumentSerializable.ReadXmlContent(IXDLSContentReader reader)
		{
			if (!this.XDLSHolder.ReadHolder(reader))
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					return this.ReadXmlContent(reader);
				}
			}
			return true;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00009F10 File Offset: 0x00008F10
		XDLSHolder IDocumentSerializable.XDLSHolder
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_72;
					case 1:
						this.ᜀ.Cleared = false;
						this.InitXDLSHolder();
						num = 5;
						continue;
					case 3:
						this.ᜀ = new XDLSHolder();
						num = 0;
						continue;
					case 4:
						if (true)
						{
						}
						if (this.ᜀ.Cleared)
						{
							num = 1;
							continue;
						}
						goto IL_BE;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_6A;
						}
						break;
					}
					IL_28:
					if (this.ᜀ == null)
					{
						num = 3;
						continue;
					}
					goto IL_72;
					goto IL_28;
					IL_72:
					num = 4;
				}
				IL_6A:
				if (false)
				{
				}
				IL_BE:
				return this.ᜀ;
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00009FE4 File Offset: 0x00008FE4
		void IDocumentSerializable.RestoreReference(string name, int value)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.RestoreReference(name, value);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000A028 File Offset: 0x00009028
		internal object ឱ()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return this.CloneImpl();
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000A06C File Offset: 0x0000906C
		internal virtual void CloneRelationsTo(Document doc, OwnerHolder nextOwner)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000095 RID: 149 RVA: 0x0000A0A8 File Offset: 0x000090A8
		protected XDLSHolder XDLSHolder
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return ((IDocumentSerializable)this).XDLSHolder;
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000A0EC File Offset: 0x000090EC
		protected virtual object CloneImpl()
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			DocumentSerializable documentSerializable = (DocumentSerializable)base.MemberwiseClone();
			documentSerializable.ᜀ = null;
			documentSerializable.ᜀ(null);
			return documentSerializable;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x0000A144 File Offset: 0x00009144
		protected virtual void WriteXmlAttributes(IXDLSAttributeWriter writer)
		{
			int a_ = 5;
			if (true)
			{
			}
			if (writer == null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					throw new ArgumentNullException(ClipboardData.b("ᱪὬٮհᙲݴ", a_));
				}
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x0000A1A4 File Offset: 0x000091A4
		protected virtual void WriteXmlContent(IXDLSContentWriter writer)
		{
			int a_ = 18;
			if (writer == null)
			{
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					throw new ArgumentNullException(ClipboardData.b("ཷࡹᕻ੽", a_));
				}
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000A204 File Offset: 0x00009204
		protected virtual void ReadXmlAttributes(IXDLSAttributeReader reader)
		{
			int a_ = 10;
			if (reader == null)
			{
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					throw new ArgumentNullException(ClipboardData.b("ɯ᝱ᕳትᵷࡹ", a_));
				}
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000A264 File Offset: 0x00009264
		protected virtual bool ReadXmlContent(IXDLSContentReader reader)
		{
			int a_ = 12;
			if (reader == null)
			{
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					throw new ArgumentNullException(ClipboardData.b("qᅳ᝵ᱷό๻", a_));
				}
			}
			return false;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000A2C4 File Offset: 0x000092C4
		protected virtual void InitXDLSHolder()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000A300 File Offset: 0x00009300
		protected virtual void RestoreReference(string name, int index)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
		}

		// Token: 0x04000932 RID: 2354
		private float \u2460\u008B\u007F\u008E;

		// Token: 0x04000933 RID: 2355
		private long \u25D8\u009D\u0082\u00AF;

		// Token: 0x04000934 RID: 2356
		private bool \u25D8\u008A\u0095\u0097;

		// Token: 0x04000935 RID: 2357
		private bool \u2593\u00AB\u00A1\u009B;

		// Token: 0x04000936 RID: 2358
		private string[] \u2593\u0084\u0085\u00A9;

		// Token: 0x04000937 RID: 2359
		private new XDLSHolder ᜀ;
	}
}
