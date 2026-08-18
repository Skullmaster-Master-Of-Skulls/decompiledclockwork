using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Documents.XML;
using Spire.Doc.Interface;

namespace Spire.Doc.Formatting
{
	// Token: 0x02000194 RID: 404
	public abstract class FormatBase : DocumentSerializable
	{
		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000F2B RID: 3883 RVA: 0x000EEE88 File Offset: 0x000EDE88
		// (set) Token: 0x06000F2C RID: 3884 RVA: 0x000EEECC File Offset: 0x000EDECC
		internal bool IsDefault
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
				return this.ᜇ;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							this.ᜂ();
							num = 2;
							continue;
						}
						break;
					case 2:
						return;
					}
					if (true)
					{
					}
					if (value)
					{
						break;
					}
					num = 0;
				}
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000F2D RID: 3885 RVA: 0x000EEF40 File Offset: 0x000EDF40
		internal Dictionary<int, object> PropertiesHash
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							this.m_propertiesHash = new Dictionary<int, object>();
							if (true)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 2:
						goto IL_6F;
					}
					if (this.m_propertiesHash != null)
					{
						break;
					}
					num = 0;
				}
				IL_6F:
				return this.m_propertiesHash;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000F2E RID: 3886 RVA: 0x000EEFC4 File Offset: 0x000EDFC4
		// (set) Token: 0x06000F2F RID: 3887 RVA: 0x000EF008 File Offset: 0x000EE008
		internal FormatBase BaseFormat
		{
			get
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
				return this.ᜄ;
			}
			set
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
				this.ᜄ = value;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000F30 RID: 3888 RVA: 0x000EF04C File Offset: 0x000EE04C
		internal int KeysOffset
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
				return this.m_keysOffset;
			}
		}

		// Token: 0x17000252 RID: 594
		protected object this[int key]
		{
			get
			{
				object obj;
				for (;;)
				{
					this.ᜀ(key);
					int fullKey = this.GetFullKey(key);
					obj = null;
					int num = 12;
					for (;;)
					{
						switch (num)
						{
						case 0:
							obj = this.ᜁ(key);
							num = 6;
							continue;
						case 1:
							return obj;
						case 2:
							if (obj == null)
							{
								num = 3;
								continue;
							}
							return obj;
						case 3:
							obj = this.GetDefValue(key);
							goto IL_156;
						case 4:
							if (this.ᜂ(key))
							{
								num = 5;
								continue;
							}
							goto IL_DB;
						case 5:
							obj = (this as CharacterFormat).CharStyle.CharacterFormat[key];
							num = 15;
							continue;
						case 6:
							if (true)
							{
							}
							goto IL_10E;
						case 7:
							goto IL_183;
						case 8:
							if (this.BaseFormat.m_propertiesHash != null)
							{
								num = 0;
								continue;
							}
							goto IL_10E;
						case 9:
							obj = this.GetDefComposite(key);
							num = 11;
							continue;
						case 10:
							num = 8;
							continue;
						case 11:
							goto IL_183;
						case 12:
							if (!this.PropertiesHash.ContainsKey(fullKey))
							{
								num = 9;
								continue;
							}
							obj = this.PropertiesHash[fullKey];
							num = 7;
							continue;
						case 13:
							if (obj == null)
							{
								num = 14;
								continue;
							}
							goto IL_10E;
						case 14:
							num = 16;
							continue;
						case 15:
							goto IL_DB;
						case 16:
							if (this.BaseFormat != null)
							{
								num = 10;
								continue;
							}
							goto IL_10E;
						}
						break;
						IL_DB:
						num = 2;
						continue;
						IL_10E:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							IL_156:
							num = 1;
							continue;
						default:
							if (false)
							{
							}
							num = 4;
							continue;
						}
						IL_183:
						num = 13;
					}
				}
				return obj;
			}
			set
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
				int fullKey = this.GetFullKey(key);
				this.PropertiesHash[fullKey] = value;
				this.IsDefault = false;
				this.OnChange(this, key);
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000F33 RID: 3891 RVA: 0x000EF2D4 File Offset: 0x000EE2D4
		internal FormatBase ParentFormat
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
				return this.ᜅ;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000F34 RID: 3892 RVA: 0x000EF318 File Offset: 0x000EE318
		internal List<Stream> XmlProps2010
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							this.ᜈ = new List<Stream>();
							if (true)
							{
							}
							num = 1;
							continue;
						}
						break;
					case 1:
						goto IL_6F;
					}
					if (this.ᜈ != null)
					{
						break;
					}
					num = 0;
				}
				IL_6F:
				return this.ᜈ;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000F35 RID: 3893 RVA: 0x000EF39C File Offset: 0x000EE39C
		internal List<XmlNode> XmlProps
		{
			get
			{
				if (true)
				{
				}
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							this.ᜉ = new List<XmlNode>();
							num = 2;
							continue;
						}
						break;
					case 2:
						goto IL_6F;
					}
					if (this.ᜉ != null)
					{
						break;
					}
					num = 1;
				}
				IL_6F:
				return this.ᜉ;
			}
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x000EF420 File Offset: 0x000EE420
		public FormatBase() : this(null)
		{
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x000EF434 File Offset: 0x000EE434
		public FormatBase(IDocument doc) : this(doc, null)
		{
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x000EF44C File Offset: 0x000EE44C
		public FormatBase(IDocument doc, DocumentObject owner)
		{
			this.ᜇ = true;
			base..ctor(doc as Document, owner);
			this.m_propertiesHash = new Dictionary<int, object>();
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x000EF478 File Offset: 0x000EE478
		public FormatBase(FormatBase parent, int parentKey)
		{
			int a_ = 8;
			this..ctor(null);
			if (parent.KeysOffset + 8 > 32)
			{
				throw new ArgumentOutOfRangeException(ClipboardData.b("ŭᙯᑱݳ፵౷", a_));
			}
			if (parentKey > 128)
			{
				throw new ArgumentOutOfRangeException(ClipboardData.b("ṭᅯqᅳᡵ౷ㅹ᥻ݽ", a_));
			}
			this.m_propertiesHash = parent.PropertiesHash;
			this.ᜆ = parentKey;
			this.ᜅ = parent;
			this.m_keysOffset = parent.KeysOffset + 8;
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x000EF500 File Offset: 0x000EE500
		public FormatBase(FormatBase parent, int parentKey, int parentOffset) : this(parent, parentKey)
		{
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x000EF518 File Offset: 0x000EE518
		protected internal void ImportContainer(FormatBase format)
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_5F;
				case 1:
					if (!(format is RowFormat))
					{
						num = 2;
						continue;
					}
					goto IL_4A;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5F;
					default:
						if (false)
						{
						}
						this.ᜃ(format);
						num = 7;
						continue;
					}
					break;
				case 4:
					if (!(format is CharacterFormat))
					{
						num = 6;
						continue;
					}
					goto IL_4A;
				case 5:
					this.m_propsUpdateFlags.Clear();
					num = 8;
					continue;
				case 6:
					num = 1;
					continue;
				case 7:
					goto IL_4A;
				case 8:
					goto IL_EC;
				case 9:
					num = 4;
					continue;
				}
				if (!(format is ParagraphFormat))
				{
					num = 9;
					continue;
				}
				IL_4A:
				this.EnsureComposites();
				this.IsDefault = false;
				num = 0;
				continue;
				IL_5F:
				if (this.m_propsUpdateFlags == null)
				{
					break;
				}
				if (true)
				{
				}
				num = 5;
			}
			IL_EC:
			this.ImportMembers(format);
			this.ᜀ(format);
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x000EF648 File Offset: 0x000EE648
		protected virtual void ImportMembers(FormatBase format)
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

		// Token: 0x06000F3D RID: 3901 RVA: 0x000EF684 File Offset: 0x000EE684
		internal virtual void ApplyBase(FormatBase baseFormat)
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
			this.ᜄ = baseFormat;
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x000EF6C8 File Offset: 0x000EE6C8
		public bool HasKey(int key)
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
				if (this.PropertiesHash != null)
				{
					return this.PropertiesHash.ContainsKey(this.GetFullKey(key));
				}
				break;
			}
			return false;
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x000EF724 File Offset: 0x000EE724
		internal bool ᜉ(int A_0)
		{
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if ((bool)this.PropertiesHash[A_0])
					{
						num = 2;
						continue;
					}
					goto IL_95;
				case 1:
					if (this.PropertiesHash.ContainsKey(A_0))
					{
						num = 3;
						continue;
					}
					goto IL_95;
				case 2:
					return true;
				case 3:
					goto IL_4C;
				case 4:
					return false;
				case 5:
					if (true)
					{
					}
					break;
				}
				if (this.PropertiesHash == null)
				{
					num = 4;
					continue;
				}
				num = 1;
				continue;
				IL_4C:
				num = 0;
				continue;
				IL_95:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_4C;
				default:
					goto IL_AB;
				}
			}
			return false;
			IL_AB:
			if (false)
			{
			}
			return false;
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x000EF7E4 File Offset: 0x000EE7E4
		public void ClearFormatting()
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
			this.m_propertiesHash.Clear();
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x000EF82C File Offset: 0x000EE82C
		protected void SetPropUpdateFlag(int propKey)
		{
			for (;;)
			{
				this.CheckUpdateFlagsColl();
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						if (!this.m_propsUpdateFlags.ContainsKey(propKey))
						{
							num = 1;
							continue;
						}
						return;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							this.m_propsUpdateFlags.Add(propKey, true);
							num = 2;
							continue;
						}
						break;
					case 2:
						return;
					}
					break;
				}
			}
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x000EF8B8 File Offset: 0x000EE8B8
		protected bool IsPropertyUpdated(int propertyKey)
		{
			for (;;)
			{
				this.CheckUpdateFlagsColl();
				if (!this.m_propsUpdateFlags.ContainsKey(propertyKey))
				{
					goto IL_3E;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_36;
				}
			}
			IL_36:
			if (false)
			{
			}
			return true;
			IL_3E:
			if (true)
			{
			}
			return false;
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x000EF90C File Offset: 0x000EE90C
		protected void CheckUpdateFlagsColl()
		{
			int num = 2;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					return;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						this.m_propsUpdateFlags = new Dictionary<int, bool>();
						num = 0;
						continue;
					}
					break;
				}
				if (this.m_propsUpdateFlags != null)
				{
					break;
				}
				num = 1;
			}
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x000EF98C File Offset: 0x000EE98C
		internal void ᜀ(short A_0, byte A_1)
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 5;
					continue;
				case 1:
				{
					spr\u1CC1 spr_u1CC;
					if (spr_u1CC != null)
					{
						num = 6;
						continue;
					}
					return;
				}
				case 2:
					goto IL_F9;
				case 4:
					if (A_1 > 1)
					{
						num = 0;
						continue;
					}
					goto IL_7A;
				case 5:
					IL_E4:
					if (A_1 >= 128)
					{
						num = 7;
						continue;
					}
					goto IL_F9;
				case 6:
				{
					if (true)
					{
					}
					spr\u1CC1 spr_u1CC;
					spr_u1CC.ᜀ(A_1);
					num = 9;
					continue;
				}
				case 7:
					goto IL_7A;
				case 8:
				{
					if (A_1 > 129)
					{
						num = 2;
						continue;
					}
					int sprmOption = this.GetSprmOption((int)A_0);
					spr\u1CC1 spr_u1CC = this.ᜊ.ᜇ(sprmOption);
					num = 1;
					continue;
				}
				case 9:
					goto IL_CD;
				case 10:
					num = 4;
					continue;
				}
				if (A_1 >= 0)
				{
					num = 10;
					continue;
				}
				goto IL_F9;
				IL_7A:
				num = 8;
				continue;
				IL_F9:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_E4;
				default:
					goto IL_10F;
				}
			}
			IL_CD:
			return;
			IL_10F:
			if (false)
			{
			}
		}

		// Token: 0x06000F45 RID: 3909
		protected abstract object GetDefValue(int key);

		// Token: 0x06000F46 RID: 3910 RVA: 0x000EFAB0 File Offset: 0x000EEAB0
		protected virtual FormatBase GetDefComposite(int key)
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
			return null;
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x000EFAEC File Offset: 0x000EEAEC
		protected virtual void OnChange(FormatBase format, int propKey)
		{
			if (true)
			{
			}
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						this.ParentFormat.OnChange(format, propKey);
						num = 1;
						continue;
					}
					break;
				case 1:
					return;
				}
				if (this.ᜅ == null)
				{
					break;
				}
				num = 0;
			}
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x000EFB6C File Offset: 0x000EEB6C
		internal virtual bool HasValue(int propertyKey)
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
			return false;
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x000EFBA8 File Offset: 0x000EEBA8
		protected virtual int GetSprmOption(int propertyKey)
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
			return int.MaxValue;
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x000EFBE8 File Offset: 0x000EEBE8
		internal virtual void Close()
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.m_propsUpdateFlags.Clear();
					this.m_propsUpdateFlags = null;
					num = 8;
					continue;
				case 1:
					return;
				case 3:
					goto IL_8B;
				case 4:
					if (true)
					{
					}
					this.ᜊ = null;
					num = 1;
					continue;
				case 5:
					if (this.ᜊ != null)
					{
						num = 4;
						continue;
					}
					return;
				case 6:
					if (this.ᜉ != null)
					{
						num = 9;
						continue;
					}
					goto IL_FD;
				case 7:
					goto IL_96;
				case 8:
					goto IL_71;
				case 9:
					this.ᜉ.Clear();
					this.ᜉ = null;
					num = 10;
					continue;
				case 10:
					goto IL_FD;
				case 11:
					this.m_propertiesHash.Clear();
					this.m_propertiesHash = null;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_96;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				}
				if (this.m_propertiesHash != null)
				{
					num = 11;
					continue;
				}
				goto IL_8B;
				IL_71:
				num = 6;
				continue;
				IL_96:
				if (this.m_propsUpdateFlags != null)
				{
					num = 0;
					continue;
				}
				goto IL_71;
				IL_8B:
				num = 7;
				continue;
				IL_FD:
				num = 5;
			}
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x000EFD48 File Offset: 0x000EED48
		protected internal virtual void EnsureComposites()
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

		// Token: 0x06000F4C RID: 3916 RVA: 0x000EFD84 File Offset: 0x000EED84
		protected void EnsureComposites(params int[] keys)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					int num = 0;
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_36;
						case 1:
							goto IL_36;
						case 2:
							goto IL_6A;
						case 3:
						{
							IL_5B:
							if (num >= keys.Length)
							{
								num2 = 2;
								continue;
							}
							int key = keys[num];
							FormatBase defComposite = this.GetDefComposite(key);
							defComposite.EnsureComposites();
							defComposite.IsDefault = false;
							num++;
							num2 = 0;
							continue;
						}
						}
						break;
						IL_36:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_5B;
						default:
							if (false)
							{
							}
							num2 = 3;
							break;
						}
					}
				}
				IL_6A:
				if (true)
				{
				}
				return;
			}
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x000EFE3C File Offset: 0x000EEE3C
		protected int GetBaseKey(int key)
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
			return key - (this.ᜆ << this.m_keysOffset);
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x000EFE8C File Offset: 0x000EEE8C
		protected int GetFullKey(int key)
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
			return key + (this.ᜆ << this.m_keysOffset);
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x000EFEE4 File Offset: 0x000EEEE4
		protected FormatBase GetDefComposite(int key, FormatBase value)
		{
			for (;;)
			{
				int fullKey = this.GetFullKey(key);
				this.PropertiesHash[fullKey] = value;
				int num = 7;
				for (;;)
				{
					if (true)
					{
					}
					FormatBase baseFormat;
					switch (num)
					{
					case 0:
						goto IL_F1;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_A6;
						default:
							if (false)
							{
							}
							if (this.BaseFormat.PropertiesHash != null)
							{
								num = 4;
								continue;
							}
							return value;
						}
						break;
					case 2:
						if (this.BaseFormat.PropertiesHash.ContainsKey(fullKey))
						{
							num = 3;
							continue;
						}
						baseFormat = (this.BaseFormat[fullKey] as FormatBase);
						num = 0;
						continue;
					case 3:
						baseFormat = (this.BaseFormat.PropertiesHash[fullKey] as FormatBase);
						num = 8;
						continue;
					case 4:
						num = 2;
						continue;
					case 5:
						goto IL_A6;
					case 6:
						return value;
					case 7:
						if (this.BaseFormat != null)
						{
							num = 5;
							continue;
						}
						return value;
					case 8:
						goto IL_F1;
					}
					break;
					IL_A6:
					num = 1;
					continue;
					IL_F1:
					value.ApplyBase(baseFormat);
					num = 6;
				}
			}
			return value;
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x000F0028 File Offset: 0x000EF028
		private void ᜂ()
		{
			for (;;)
			{
				this.ᜇ = false;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							this.ᜅ.IsDefault = false;
							num = 1;
							continue;
						}
						break;
					case 1:
						return;
					case 2:
						if (this.ᜅ != null)
						{
							num = 0;
							continue;
						}
						return;
					}
					break;
				}
			}
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x000F00B0 File Offset: 0x000EF0B0
		internal bool \u1739()
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return true;
				case 1:
					IL_4E:
					if (this.ᜉ.Count != 0)
					{
						num = 0;
						continue;
					}
					goto IL_65;
				case 3:
					num = 1;
					continue;
				}
				if (true)
				{
				}
				if (this.ᜉ != null)
				{
					num = 3;
					continue;
				}
				IL_65:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_4E;
				default:
					goto IL_7B;
				}
			}
			return true;
			IL_7B:
			if (false)
			{
			}
			return false;
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x000F0140 File Offset: 0x000EF140
		internal virtual void RemoveChanges()
		{
			for (;;)
			{
				IL_00:
				switch (0)
				{
				default:
				{
					int num = 13;
					for (;;)
					{
						int num2;
						switch (num)
						{
						case 0:
						{
							if (num2 == 0)
							{
								num = 7;
								continue;
							}
							spr\u1CC1 spr_u1CC = this.ᜊ.ᜇ(num2);
							num = 6;
							continue;
						}
						case 1:
							goto IL_E9;
						case 2:
							this.m_propsUpdateFlags.Clear();
							num = 1;
							continue;
						case 3:
							goto IL_179;
						case 4:
							goto IL_179;
						case 5:
							if (this.m_propertiesHash != null)
							{
								num = 10;
								continue;
							}
							return;
						case 6:
						{
							spr\u1CC1 spr_u1CC;
							if (spr_u1CC == null)
							{
								return;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (false)
								{
								}
								num = 12;
								continue;
							}
							break;
						}
						case 7:
							return;
						case 8:
							goto IL_71;
						case 9:
							return;
						case 10:
							this.m_propertiesHash.Clear();
							num = 9;
							continue;
						case 11:
						{
							int num3;
							if (num3 >= this.ᜊ.ᜂ().Count)
							{
								num = 14;
								continue;
							}
							this.ᜊ.ᜂ().RemoveAt(num3);
							num3--;
							num3++;
							num = 3;
							continue;
						}
						case 12:
						{
							spr\u1CC1 spr_u1CC;
							int num4 = this.ᜊ.ᜂ().IndexOf(spr_u1CC);
							int num3 = num4;
							num = 4;
							continue;
						}
						case 14:
							num = 15;
							continue;
						case 15:
							if (this.m_propsUpdateFlags != null)
							{
								num = 2;
								continue;
							}
							goto IL_E9;
						}
						if (this.ᜊ == null)
						{
							num = 8;
							continue;
						}
						num2 = this.ᜀ();
						num = 0;
						continue;
						IL_E9:
						num = 5;
						continue;
						IL_179:
						num = 11;
					}
					break;
				}
				}
			}
			IL_71:
			if (true)
			{
			}
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x000F0344 File Offset: 0x000EF344
		internal virtual void AcceptChanges()
		{
			switch (0)
			{
			default:
			{
				int num = 31;
				for (;;)
				{
					int num2;
					spr\u1CC1 spr_u1CC;
					int num6;
					int num7;
					switch (num)
					{
					case 0:
						goto IL_244;
					case 1:
						if (this.ᜊ.ᜇ() == 0)
						{
							num = 18;
							continue;
						}
						num2 = this.ᜀ();
						num = 11;
						continue;
					case 2:
						goto IL_28D;
					case 3:
					{
						List<spr\u1CC1> list;
						List<spr\u1CC1>.Enumerator enumerator = list.GetEnumerator();
						num = 23;
						continue;
					}
					case 4:
					{
						int a_;
						if (this.ᜊ.ᜂ(a_))
						{
							num = 12;
							continue;
						}
						int num3 = this.ᜊ.ᜂ().IndexOf(spr_u1CC) + 1;
						List<spr\u1CC1> list = null;
						num = 7;
						continue;
					}
					case 5:
						num = 1;
						continue;
					case 6:
						if (this is ParagraphFormat)
						{
							num = 30;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_23F;
						default:
						{
							if (false)
							{
							}
							int a_;
							this.ᜊ.ᜆ(a_);
							num = 28;
							continue;
						}
						}
						break;
					case 7:
					{
						int num3;
						if (num3 < this.ᜊ.ᜈ())
						{
							num = 19;
							continue;
						}
						goto IL_15C;
					}
					case 8:
						if (true)
						{
						}
						this.m_propsUpdateFlags.Clear();
						num = 29;
						continue;
					case 9:
					{
						int a_;
						this.ᜊ.ᜆ(a_);
						num = 0;
						continue;
					}
					case 10:
						goto IL_28D;
					case 11:
						if (num2 == 0)
						{
							num = 15;
							continue;
						}
						spr_u1CC = this.ᜊ.ᜃ(num2);
						num = 24;
						continue;
					case 12:
						goto IL_23F;
					case 13:
						goto IL_288;
					case 14:
						goto IL_244;
					case 15:
						return;
					case 16:
					{
						int num4;
						int num5;
						if (num4 >= num5)
						{
							num = 3;
							continue;
						}
						List<spr\u1CC1> list;
						list.Add(this.ᜊ.ᜁ(num4));
						num4++;
						num = 17;
						continue;
					}
					case 17:
						goto IL_BF;
					case 18:
						return;
					case 19:
					{
						List<spr\u1CC1> list = new List<spr\u1CC1>();
						int num3;
						int num4 = num3;
						int num5 = this.ᜊ.ᜈ();
						num = 21;
						continue;
					}
					case 20:
						if (num6 > num7)
						{
							num = 25;
							continue;
						}
						this.ᜊ.ᜂ().RemoveAt(0);
						num6++;
						num = 10;
						continue;
					case 21:
						goto IL_BF;
					case 22:
						if (this.ᜊ.ᜂ(17931))
						{
							num = 9;
							continue;
						}
						goto IL_244;
					case 23:
						try
						{
							num = 1;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_479;
								case 2:
								{
									List<spr\u1CC1>.Enumerator enumerator;
									if (!enumerator.MoveNext())
									{
										num = 3;
										continue;
									}
									spr\u1CC1 spr_u1CC2 = enumerator.Current;
									this.ᜊ.ᜆ((int)spr_u1CC2.ᜂ());
									this.ᜊ.ᜆ(spr_u1CC2);
									num = 4;
									continue;
								}
								case 3:
									num = 0;
									continue;
								}
								IL_453:
								num = 2;
								continue;
								goto IL_453;
							}
							IL_479:
							goto IL_15C;
						}
						finally
						{
							List<spr\u1CC1>.Enumerator enumerator;
							((IDisposable)enumerator).Dispose();
						}
						return;
					case 24:
						if (spr_u1CC != null)
						{
							num = 27;
							continue;
						}
						return;
					case 25:
						num = 6;
						continue;
					case 26:
						if (this.m_propsUpdateFlags != null)
						{
							num = 8;
							continue;
						}
						goto IL_271;
					case 27:
					{
						int a_ = this.ᜁ();
						num = 4;
						continue;
					}
					case 28:
						goto IL_244;
					case 29:
						goto IL_271;
					case 30:
						num = 22;
						continue;
					}
					if (this.ᜊ != null)
					{
						num = 5;
						continue;
					}
					return;
					IL_BF:
					num = 16;
					continue;
					IL_15C:
					this.ᜊ.ᜆ(num2);
					num = 14;
					continue;
					IL_23F:
					num7 = this.ᜊ.ᜂ().IndexOf(spr_u1CC);
					num6 = 0;
					num = 2;
					continue;
					IL_244:
					num = 26;
					continue;
					IL_271:
					this.PropertiesHash.Clear();
					num = 13;
					continue;
					IL_28D:
					num = 20;
				}
				return;
				IL_288:
				return;
			}
			}
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x000F07F0 File Offset: 0x000EF7F0
		private int ᜁ()
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return 17920;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					}
					goto Block_3;
				case 2:
					if (this is ParagraphFormat)
					{
						num = 0;
						continue;
					}
					return 0;
				}
				if (true)
				{
				}
				if (this is CharacterFormat)
				{
					num = 1;
				}
				else
				{
					num = 2;
				}
			}
			return 17920;
			Block_3:
			if (false)
			{
			}
			return 18992;
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x000F0884 File Offset: 0x000EF884
		private int ᜀ()
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_9F;
				case 1:
					return 13928;
				case 3:
					return 9828;
				case 4:
					if (this is RowFormat)
					{
						num = 1;
						continue;
					}
					return 0;
				case 5:
					return 10883;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_9F:
					if (this is ParagraphFormat)
					{
						num = 3;
					}
					else
					{
						if (true)
						{
						}
						num = 4;
					}
					break;
				default:
					if (false)
					{
					}
					if (this is CharacterFormat)
					{
						num = 5;
					}
					else
					{
						num = 0;
					}
					break;
				}
			}
			return 10883;
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x000F0948 File Offset: 0x000EF948
		internal virtual void RemovePositioning()
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

		// Token: 0x06000F57 RID: 3927 RVA: 0x000F0984 File Offset: 0x000EF984
		private void ᜀ(FormatBase A_0)
		{
			int num = 2;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				}
				if (false)
				{
				}
				List<XmlNode>.Enumerator enumerator;
				switch (num)
				{
				case 0:
					if (A_0.ᜉ.Count > 0)
					{
						num = 3;
						continue;
					}
					return;
				case 1:
					num = 0;
					continue;
				case 3:
					goto IL_E6;
				case 4:
					try
					{
						num = 1;
						for (;;)
						{
							switch (num)
							{
							case 2:
							{
								if (!enumerator.MoveNext())
								{
									num = 4;
									continue;
								}
								XmlNode xmlNode = enumerator.Current;
								this.XmlProps.Add(xmlNode.Clone());
								num = 0;
								continue;
							}
							case 3:
								goto IL_D6;
							case 4:
								num = 3;
								continue;
							}
							IL_B3:
							num = 2;
							continue;
							goto IL_B3;
						}
						IL_D6:
						return;
					}
					finally
					{
						((IDisposable)enumerator).Dispose();
					}
					goto IL_E6;
				}
				if (true)
				{
				}
				if (A_0.ᜉ != null)
				{
					num = 1;
					continue;
				}
				break;
				IL_E6:
				enumerator = A_0.XmlProps.GetEnumerator();
				num = 4;
			}
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x000F0ACC File Offset: 0x000EFACC
		internal void ᜃ(FormatBase A_0)
		{
			for (;;)
			{
				Dictionary<int, object> dictionary = A_0.PropertiesHash;
				this.m_propertiesHash = new Dictionary<int, object>(dictionary.Count);
				IDictionaryEnumerator dictionaryEnumerator = dictionary.GetEnumerator();
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (!dictionaryEnumerator.MoveNext())
						{
							num = 2;
							continue;
						}
						this.m_propertiesHash.Add((int)dictionaryEnumerator.Key, dictionaryEnumerator.Value);
						num = 3;
						continue;
					case 1:
						goto IL_46;
					case 2:
						return;
					case 3:
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
							goto IL_46;
						}
						break;
					}
					break;
					IL_46:
					num = 0;
				}
			}
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x000F0B90 File Offset: 0x000EFB90
		internal void ᜄ(FormatBase A_0)
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
			this.m_propertiesHash = A_0.PropertiesHash;
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x000F0BD8 File Offset: 0x000EFBD8
		internal void ᜂ(FormatBase A_0)
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
				using (Dictionary<int, object>.Enumerator enumerator = A_0.PropertiesHash.GetEnumerator())
				{
					int num = 15;
					for (;;)
					{
						switch (num)
						{
						case 0:
							num = 13;
							continue;
						case 1:
						{
							KeyValuePair<int, object> keyValuePair;
							if (!(keyValuePair.Value is RowFormat.TablePositioning))
							{
								num = 0;
								continue;
							}
							break;
						}
						case 2:
							num = 9;
							continue;
						case 3:
						{
							KeyValuePair<int, object> keyValuePair;
							this.PropertiesHash[keyValuePair.Key] = keyValuePair.Value;
							num = 5;
							continue;
						}
						case 4:
						{
							KeyValuePair<int, object> keyValuePair;
							if (!(keyValuePair.Value is Paddings))
							{
								num = 11;
								continue;
							}
							break;
						}
						case 6:
						{
							KeyValuePair<int, object> keyValuePair;
							if (!(keyValuePair.Value is Borders))
							{
								num = 14;
								continue;
							}
							break;
						}
						case 7:
						{
							KeyValuePair<int, object> keyValuePair;
							if (!(keyValuePair.Value is Border))
							{
								num = 8;
								continue;
							}
							break;
						}
						case 8:
							num = 4;
							continue;
						case 9:
							goto IL_1D7;
						case 10:
						{
							if (!enumerator.MoveNext())
							{
								num = 2;
								continue;
							}
							KeyValuePair<int, object> keyValuePair = enumerator.Current;
							num = 6;
							continue;
						}
						case 11:
							num = 1;
							continue;
						case 13:
						{
							KeyValuePair<int, object> keyValuePair;
							if (this.PropertiesHash.ContainsKey(keyValuePair.Key))
							{
								num = 3;
								continue;
							}
							this.PropertiesHash.Add(keyValuePair.Key, keyValuePair.Value);
							num = 12;
							continue;
						}
						case 14:
							num = 7;
							continue;
						}
						IL_127:
						num = 10;
						continue;
						goto IL_127;
					}
					IL_1D7:;
				}
				break;
			}
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x000F0DF4 File Offset: 0x000EFDF4
		private bool ᜂ(int A_0)
		{
			for (;;)
			{
				int fullKey = this.GetFullKey(A_0);
				int num = 8;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 6;
						continue;
					case 1:
						if ((this as CharacterFormat).CharStyle != null)
						{
							goto IL_119;
						}
						return false;
					case 2:
						if ((this as CharacterFormat).CharStyle.CharacterFormat[A_0] != null)
						{
							if (true)
							{
							}
							num = 9;
							continue;
						}
						return false;
					case 3:
						num = 2;
						continue;
					case 4:
						return true;
					case 5:
						num = 1;
						continue;
					case 6:
						if (!this.PropertiesHash.ContainsKey(fullKey))
						{
							num = 5;
							continue;
						}
						return false;
					case 7:
						if ((this as CharacterFormat).CharStyle.CharacterFormat.HasValue(A_0))
						{
							num = 4;
							continue;
						}
						return false;
					case 8:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_119;
						default:
							if (false)
							{
							}
							if (this is CharacterFormat)
							{
								num = 0;
								continue;
							}
							return false;
						}
						break;
					case 9:
						num = 7;
						continue;
					}
					break;
					IL_119:
					num = 3;
				}
			}
			return true;
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x000F0F38 File Offset: 0x000EFF38
		private object ᜁ(int A_0)
		{
			switch (0)
			{
			default:
			{
				object obj;
				for (;;)
				{
					obj = this.BaseFormat[A_0];
					int num = 6;
					for (;;)
					{
						ListFormat listFormat;
						FormatBase formatBase;
						int fullKey;
						switch (num)
						{
						case 0:
							if ((this as ParagraphFormat).OwnerBase is ParagraphStyle)
							{
								num = 21;
								continue;
							}
							goto IL_22B;
						case 1:
							num = 28;
							continue;
						case 2:
							if (obj is bool)
							{
								num = 4;
								continue;
							}
							goto IL_158;
						case 3:
							if (A_0 == 70)
							{
								num = 25;
								continue;
							}
							return obj;
						case 4:
							goto IL_19F;
						case 5:
							goto IL_158;
						case 6:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_34F;
							default:
								if (false)
								{
								}
								if (this is CharacterFormat)
								{
									num = 35;
									continue;
								}
								goto IL_19F;
							}
							break;
						case 7:
							if ((this as CharacterFormat).TableStyleCharacterFormat != null)
							{
								num = 34;
								continue;
							}
							goto IL_19F;
						case 8:
							num = 15;
							continue;
						case 9:
							goto IL_34F;
						case 10:
							num = 36;
							continue;
						case 11:
							if (this is ParagraphFormat)
							{
								num = 10;
								continue;
							}
							goto IL_1EB;
						case 12:
							listFormat = ((this as ParagraphFormat).OwnerBase as Paragraph).ListFormat;
							num = 9;
							continue;
						case 13:
							if (true)
							{
							}
							formatBase = formatBase.BaseFormat;
							num = 31;
							continue;
						case 14:
							num = 3;
							continue;
						case 15:
							if (A_0 != 68)
							{
								num = 14;
								continue;
							}
							goto IL_103;
						case 16:
							if ((this as ParagraphFormat).OwnerBase is Paragraph)
							{
								num = 12;
								continue;
							}
							num = 0;
							continue;
						case 17:
							goto IL_328;
						case 18:
							if (this is CharacterFormat)
							{
								num = 20;
								continue;
							}
							goto IL_24D;
						case 19:
							goto IL_354;
						case 20:
							goto IL_3C9;
						case 21:
							listFormat = ((this as ParagraphFormat).OwnerBase as ParagraphStyle).ListFormat;
							num = 30;
							continue;
						case 22:
							if (formatBase.PropertiesHash.ContainsKey(fullKey))
							{
								num = 37;
								continue;
							}
							num = 33;
							continue;
						case 23:
							if (listFormat != null)
							{
								num = 32;
								continue;
							}
							return obj;
						case 24:
							num = 29;
							continue;
						case 25:
							goto IL_103;
						case 26:
							if (this is ParagraphFormat)
							{
								num = 24;
								continue;
							}
							return obj;
						case 27:
							if (listFormat.IsEmptyList)
							{
								num = 17;
								continue;
							}
							return obj;
						case 28:
							if (A_0 != 5)
							{
								num = 8;
								continue;
							}
							goto IL_103;
						case 29:
							if (A_0 != 2)
							{
								num = 1;
								continue;
							}
							goto IL_103;
						case 30:
							goto IL_22B;
						case 31:
							goto IL_354;
						case 32:
							num = 27;
							continue;
						case 33:
							if (formatBase.BaseFormat != null)
							{
								num = 13;
								continue;
							}
							num = 18;
							continue;
						case 34:
							num = 2;
							continue;
						case 35:
							num = 7;
							continue;
						case 36:
							if ((this as ParagraphFormat).TableStyleParagraphFormat != null)
							{
								num = 5;
								continue;
							}
							goto IL_1EB;
						case 37:
							return obj;
						}
						break;
						IL_103:
						listFormat = null;
						num = 16;
						continue;
						IL_158:
						formatBase = this.BaseFormat;
						fullKey = this.GetFullKey(A_0);
						num = 19;
						continue;
						IL_19F:
						num = 11;
						continue;
						IL_1EB:
						num = 26;
						continue;
						IL_22B:
						num = 23;
						continue;
						IL_34F:
						goto IL_22B;
						IL_354:
						num = 22;
					}
				}
				return obj;
				IL_24D:
				return (this as ParagraphFormat).TableStyleParagraphFormat[A_0];
				IL_328:
				return null;
				IL_3C9:
				return (this as CharacterFormat).TableStyleCharacterFormat[A_0];
			}
			}
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x000F1394 File Offset: 0x000F0394
		private void ᜀ(int A_0)
		{
			int num = 8;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0 != 20)
					{
						num = 4;
						continue;
					}
					return;
				case 1:
					goto IL_AE;
				case 2:
					if (A_0 != 67)
					{
						num = 3;
						continue;
					}
					goto IL_60;
				case 3:
					goto IL_C9;
				case 4:
					(this as ParagraphFormat).ᜆ(A_0);
					num = 5;
					continue;
				case 5:
					return;
				case 6:
					num = 0;
					continue;
				case 7:
					if (this is ParagraphFormat)
					{
						num = 6;
						continue;
					}
					return;
				}
				if (this is CharacterFormat)
				{
					num = 1;
					continue;
				}
				IL_60:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_AE:
					num = 2;
					continue;
				}
				if (true)
				{
				}
				if (false)
				{
				}
				num = 7;
			}
			IL_C9:
			(this as CharacterFormat).ᜇ(A_0);
		}

		// Token: 0x04001755 RID: 5973
		private new const int ᜀ = 8;

		// Token: 0x04001756 RID: 5974
		private byte[] \u2609\u0092\u00B0ª;

		// Token: 0x04001757 RID: 5975
		private const int ᜁ = 4;

		// Token: 0x04001758 RID: 5976
		private const int ᜂ = 32;

		// Token: 0x04001759 RID: 5977
		private const int ᜃ = 128;

		// Token: 0x0400175A RID: 5978
		protected Dictionary<int, object> m_propertiesHash;

		// Token: 0x0400175B RID: 5979
		private FormatBase ᜄ;

		// Token: 0x0400175C RID: 5980
		private string \u2609\u0089\u00AB\u0085;

		// Token: 0x0400175D RID: 5981
		private int \u2609\u00AD\u009A\u009B;

		// Token: 0x0400175E RID: 5982
		private FormatBase ᜅ;

		// Token: 0x0400175F RID: 5983
		private int ᜆ;

		// Token: 0x04001760 RID: 5984
		protected int m_keysOffset;

		// Token: 0x04001761 RID: 5985
		private bool ᜇ;

		// Token: 0x04001762 RID: 5986
		protected Dictionary<int, bool> m_propsUpdateFlags;

		// Token: 0x04001763 RID: 5987
		private List<Stream> ᜈ;

		// Token: 0x04001764 RID: 5988
		private List<XmlNode> ᜉ;

		// Token: 0x04001765 RID: 5989
		internal sprḍ ᜊ;
	}
}
