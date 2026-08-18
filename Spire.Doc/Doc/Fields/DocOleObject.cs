using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Spire.CompoundFile.Doc;
using Spire.CompoundFile.Doc.Native;
using Spire.Doc.Documents;
using Spire.Layouting;

namespace Spire.Doc.Fields
{
	// Token: 0x02000517 RID: 1303
	public class DocOleObject : ParagraphBase, spr\u2297
	{
		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06004362 RID: 17250 RVA: 0x003F3580 File Offset: 0x003F2580
		// (set) Token: 0x06004363 RID: 17251 RVA: 0x003F35C4 File Offset: 0x003F25C4
		public bool DisplayAsIcon
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
				return this.ᜐ;
			}
			set
			{
				for (;;)
				{
					this.ᜐ = value;
					if (true)
					{
					}
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (!base.Document.ᜇ)
							{
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_66;
								}
								if (false)
								{
								}
								num = 2;
								continue;
							}
							return;
						case 1:
							return;
						case 2:
							goto IL_66;
						}
						break;
						IL_66:
						this.ᜁ();
						num = 1;
					}
				}
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06004364 RID: 17252 RVA: 0x003F364C File Offset: 0x003F264C
		public DocPicture OlePicture
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
				return this.ᜂ;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06004365 RID: 17253 RVA: 0x003F3690 File Offset: 0x003F2690
		public override DocumentObjectType DocumentObjectType
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
				return DocumentObjectType.OleObject;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06004366 RID: 17254 RVA: 0x003F36D0 File Offset: 0x003F26D0
		public Stream Container
		{
			get
			{
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_55;
					case 1:
						num = 5;
						continue;
					case 2:
						num = 3;
						continue;
					case 3:
						if (this.m_doc.ObjectPool.Length > 0)
						{
							num = 6;
							continue;
						}
						goto IL_C9;
					case 5:
						if (true)
						{
						}
						if (this.m_doc.ObjectPool == null)
						{
							goto IL_C9;
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
							num = 2;
							continue;
						}
						break;
					case 6:
						this.ᜃ = this.ᜄ();
						num = 0;
						continue;
					}
					IL_2C:
					if (this.ᜃ == null)
					{
						num = 1;
						continue;
					}
					break;
					goto IL_2C;
				}
				IL_55:
				IL_C9:
				return this.ᜃ;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06004367 RID: 17255 RVA: 0x003F37AC File Offset: 0x003F27AC
		// (set) Token: 0x06004368 RID: 17256 RVA: 0x003F3880 File Offset: 0x003F2880
		internal Field Field
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_6E;
					case 1:
						if (this.ᜄ.Type == FieldType.FieldNone)
						{
							num = 5;
							continue;
						}
						goto IL_B5;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_9E;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 3:
						goto IL_6C;
					case 4:
						this.ᜄ = new Field(this.m_doc);
						goto IL_9E;
					case 5:
						this.ᜉ();
						num = 3;
						continue;
					}
					if (true)
					{
					}
					if (this.ᜄ == null)
					{
						num = 4;
						continue;
					}
					IL_6E:
					num = 1;
					continue;
					IL_9E:
					num = 0;
				}
				IL_6C:
				IL_B5:
				this.ᜄ.ᜀ(this);
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

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06004369 RID: 17257 RVA: 0x003F38C4 File Offset: 0x003F28C4
		// (set) Token: 0x0600436A RID: 17258 RVA: 0x003F395C File Offset: 0x003F295C
		public string OleStorageName
		{
			get
			{
				for (;;)
				{
					IL_00:
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
								goto IL_00;
							default:
								if (true)
								{
								}
								if (false)
								{
								}
								this.ᜅ = new Random().Next().ToString();
								num = 1;
								continue;
							}
							break;
						case 1:
							goto IL_81;
						}
						if (!string.IsNullOrEmpty(this.ᜅ))
						{
							goto IL_83;
						}
						num = 0;
					}
				}
				IL_81:
				IL_83:
				return this.ᜅ;
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
				this.ᜅ = value;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x0600436B RID: 17259 RVA: 0x003F39A0 File Offset: 0x003F29A0
		// (set) Token: 0x0600436C RID: 17260 RVA: 0x003F3A24 File Offset: 0x003F2A24
		public string LinkPath
		{
			get
			{
				for (;;)
				{
					IL_00:
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_6F;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (false)
								{
								}
								this.ᜃ();
								if (true)
								{
								}
								num = 0;
								continue;
							}
							break;
						}
						if (!string.IsNullOrEmpty(this.ᜆ))
						{
							goto IL_71;
						}
						num = 1;
					}
				}
				IL_6F:
				IL_71:
				return this.ᜆ;
			}
			set
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
				value = Path.GetFullPath(value);
				this.ᜆ = value;
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x0600436D RID: 17261 RVA: 0x003F3A70 File Offset: 0x003F2A70
		public OleLinkType LinkType
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
				return this.ᜉ;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x0600436E RID: 17262 RVA: 0x003F3AB4 File Offset: 0x003F2AB4
		// (set) Token: 0x0600436F RID: 17263 RVA: 0x003F3AF8 File Offset: 0x003F2AF8
		internal spr\u24D5 OleXmlItem
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
				return this.ᜊ;
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
				this.ᜊ = value;
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06004370 RID: 17264 RVA: 0x003F3B3C File Offset: 0x003F2B3C
		// (set) Token: 0x06004371 RID: 17265 RVA: 0x003F3BDC File Offset: 0x003F2BDC
		internal OleObjectType OleObjectType
		{
			get
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
				{
					if (false)
					{
					}
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 1:
							num = 3;
							continue;
						case 2:
							goto IL_89;
						case 3:
							if (this.OleObject.ᜂ() != OleObjectType.Undefined)
							{
								num = 2;
								continue;
							}
							goto IL_8B;
						}
						if (true)
						{
						}
						if (this.OleObject == null)
						{
							goto IL_8B;
						}
						num = 1;
					}
					IL_89:
					return this.ᜋ.ᜂ();
				}
				}
				IL_8B:
				return this.ᜈ;
			}
			set
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
				this.ᜈ = value;
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06004372 RID: 17266 RVA: 0x003F3C20 File Offset: 0x003F2C20
		// (set) Token: 0x06004373 RID: 17267 RVA: 0x003F3C78 File Offset: 0x003F2C78
		public string ObjectType
		{
			get
			{
				if (this.ᜇ == null)
				{
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
						return spr\u20F5.ᜀ(this.OleObjectType, false);
					}
				}
				return this.ᜇ;
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
				this.ᜇ = value;
				this.ᜈ = spr\u20F5.ᜀ(value);
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06004374 RID: 17268 RVA: 0x003F3CC8 File Offset: 0x003F2CC8
		public byte[] NativeData
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_61;
					case 2:
						goto IL_46;
					case 3:
						if (this.OleObject != null)
						{
							num = 0;
							continue;
						}
						goto IL_11D;
					case 4:
						this.\u170D = new byte[this.ᜃ.Length];
						this.ᜃ.Read(this.\u170D, 0, this.\u170D.Length);
						num = 2;
						continue;
					case 5:
						if (this.\u170D == null)
						{
							num = 6;
							continue;
						}
						goto IL_46;
					case 6:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_46;
						default:
							if (false)
							{
							}
							num = 7;
							continue;
						}
						break;
					case 7:
						if (this.ᜃ != null)
						{
							num = 4;
							continue;
						}
						goto IL_46;
					case 8:
						num = 5;
						continue;
					}
					if (this.OleObject == null)
					{
						num = 8;
						continue;
					}
					IL_46:
					num = 3;
				}
				IL_61:
				return this.ᜋ.ᜃ();
				IL_11D:
				if (true)
				{
				}
				return this.\u170D;
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06004375 RID: 17269 RVA: 0x003F3E00 File Offset: 0x003F2E00
		private sprḴ OleObject
		{
			get
			{
				for (;;)
				{
					IL_00:
					int num = 1;
					for (;;)
					{
						if (true)
						{
						}
						switch (num)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
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
							goto IL_6A;
						}
						if (this.ᜋ != null)
						{
							goto IL_6C;
						}
						num = 0;
					}
				}
				IL_6A:
				IL_6C:
				return this.ᜋ;
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06004376 RID: 17270 RVA: 0x003F3E80 File Offset: 0x003F2E80
		private static int NextOleObjId
		{
			get
			{
				for (;;)
				{
					IL_00:
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
								goto IL_00;
							default:
								if (true)
								{
								}
								if (false)
								{
								}
								DocOleObject.ᜌ = new Random(default(DateTime).Millisecond);
								num = 2;
								continue;
							}
							break;
						case 2:
							goto IL_7E;
						}
						if (DocOleObject.ᜌ != null)
						{
							goto IL_80;
						}
						num = 0;
					}
				}
				IL_7E:
				IL_80:
				return DocOleObject.ᜌ.Next();
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06004377 RID: 17271 RVA: 0x003F3F18 File Offset: 0x003F2F18
		public string PackageFileName
		{
			get
			{
				if (this.OleObject == null)
				{
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
						return this.ᜎ;
					}
				}
				return this.OleObject.ᜀ();
			}
		}

		// Token: 0x06004378 RID: 17272 RVA: 0x003F3F70 File Offset: 0x003F2F70
		public DocOleObject(Document doc) : base(doc)
		{
			this.ᜅ = string.Empty;
			this.ᜆ = string.Empty;
		}

		// Token: 0x06004379 RID: 17273 RVA: 0x003F3FC4 File Offset: 0x003F2FC4
		private Stream ᜄ()
		{
			int a_ = 11;
			switch (0)
			{
			default:
			{
				if (true)
				{
				}
				int num = 4;
				for (;;)
				{
					MemoryStream memoryStream;
					sprᤘ sprᤘ;
					sprᤘ sprᤘ2;
					sprᤘ sprᤘ3;
					sprᤘ sprᤘ4;
					MemoryStream memoryStream2;
					switch (num)
					{
					case 0:
						num = 1;
						continue;
					case 1:
						if (this.m_doc.ObjectPool.Length == 0)
						{
							num = 2;
							continue;
						}
						goto IL_278;
					case 2:
						goto IL_2BE;
					case 3:
						try
						{
							try
							{
								memoryStream = new MemoryStream(this.m_doc.ObjectPool);
								sprᤘ = new sprᤘ(memoryStream);
								sprᤘ2 = sprᤘ.ᜆ(ClipboardData.b("㹰ᅲὴቶེ᩸⵼ၾ", a_));
								sprᤘ3 = sprᤘ2.ᜆ(ClipboardData.b("⹰", a_) + this.OleStorageName.ToString());
								sprᤘ4 = sprᤘ.ᜆ();
								sprᤘ.ᜀ(sprᤘ3, sprᤘ4);
								memoryStream2 = new MemoryStream();
								sprᤘ4.ᜀ(memoryStream2);
								memoryStream2.Position = 0L;
							}
							catch (Exception)
							{
							}
							return memoryStream2;
						}
						finally
						{
							num = 6;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_257;
								case 1:
									if (sprᤘ4 != null)
									{
										num = 5;
										continue;
									}
									goto IL_277;
								case 2:
									goto IL_200;
								case 3:
									goto IL_1DC;
								case 4:
									sprᤘ3.Close();
									sprᤘ3.Dispose();
									num = 3;
									continue;
								case 5:
									sprᤘ4.Close();
									sprᤘ4.Dispose();
									num = 9;
									continue;
								case 7:
									goto IL_20C;
								case 8:
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										goto IL_20C;
									default:
										if (false)
										{
										}
										if (sprᤘ != null)
										{
											num = 10;
											continue;
										}
										goto IL_257;
									}
									break;
								case 9:
									goto IL_181;
								case 10:
									sprᤘ.Close();
									sprᤘ2.Dispose();
									num = 0;
									continue;
								case 11:
									sprᤘ2.Close();
									sprᤘ2.Dispose();
									num = 2;
									continue;
								case 12:
									if (sprᤘ2 != null)
									{
										num = 11;
										continue;
									}
									goto IL_200;
								case 13:
									goto IL_1A0;
								case 14:
									memoryStream.Close();
									memoryStream.Dispose();
									num = 13;
									continue;
								}
								if (memoryStream != null)
								{
									num = 14;
									continue;
								}
								IL_1A0:
								num = 8;
								continue;
								IL_1DC:
								num = 1;
								continue;
								IL_20C:
								if (sprᤘ3 != null)
								{
									num = 4;
									continue;
								}
								goto IL_1DC;
								IL_200:
								num = 7;
								continue;
								IL_257:
								num = 12;
							}
							IL_181:
							IL_277:;
						}
						goto IL_278;
					}
					if (this.m_doc.ObjectPool != null)
					{
						num = 0;
						continue;
					}
					break;
					IL_278:
					memoryStream = null;
					sprᤘ = null;
					sprᤘ2 = null;
					sprᤘ3 = null;
					sprᤘ4 = null;
					memoryStream2 = null;
					num = 3;
				}
				IL_2BE:
				return null;
			}
			}
		}

		// Token: 0x0600437A RID: 17274 RVA: 0x003F42CC File Offset: 0x003F32CC
		private void ᜃ()
		{
			switch (0)
			{
			default:
			{
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_EF;
						default:
						{
							if (false)
							{
							}
							if (true)
							{
							}
							string[] array;
							this.ᜆ = array[1];
							num = 3;
							continue;
						}
						}
						break;
					case 1:
						if (string.IsNullOrEmpty(this.ᜆ))
						{
							num = 6;
							continue;
						}
						return;
					case 2:
					{
						string[] array;
						if (array.Length > 1)
						{
							num = 0;
							continue;
						}
						return;
					}
					case 3:
						return;
					case 4:
						goto IL_97;
					case 6:
						num = 2;
						continue;
					case 7:
					{
						string[] array;
						this.ᜈ = spr\u20F5.ᜀ(array[0].Trim());
						num = 4;
						continue;
					}
					case 8:
					{
						string value = this.ᜄ.Value;
						char[] separator = new char[]
						{
							'"'
						};
						string[] array = value.Split(separator);
						num = 9;
						continue;
					}
					case 9:
						goto IL_EF;
					}
					if (this.ᜄ != null)
					{
						num = 8;
						continue;
					}
					break;
					IL_97:
					num = 1;
					continue;
					IL_EF:
					if (this.ᜈ != OleObjectType.Undefined)
					{
						goto IL_97;
					}
					num = 7;
				}
				return;
			}
			}
		}

		// Token: 0x0600437B RID: 17275 RVA: 0x003F442C File Offset: 0x003F342C
		internal new void ᜀ(Stream A_0)
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
			this.ᜃ = A_0;
		}

		// Token: 0x0600437C RID: 17276 RVA: 0x003F4470 File Offset: 0x003F3470
		internal new void ᜀ(sprḴ A_0)
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
			this.ᜋ = A_0;
		}

		// Token: 0x0600437D RID: 17277 RVA: 0x003F44B4 File Offset: 0x003F34B4
		internal new void ᜀ(byte[] A_0)
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
			this.\u170D = A_0;
		}

		// Token: 0x0600437E RID: 17278 RVA: 0x003F44F8 File Offset: 0x003F34F8
		internal new void ᜀ(DocPicture A_0)
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
			this.ᜂ = A_0;
		}

		// Token: 0x0600437F RID: 17279 RVA: 0x003F453C File Offset: 0x003F353C
		internal new void ᜀ(OleLinkType A_0)
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
			this.ᜉ = A_0;
		}

		// Token: 0x06004380 RID: 17280 RVA: 0x003F4580 File Offset: 0x003F3580
		internal void ᜉ()
		{
			if (this.ᜉ != OleLinkType.Link)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_13;
				}
				if (false)
				{
				}
				this.ᜄ.Type = FieldType.FieldEmbed;
				return;
			}
			if (true)
			{
			}
			IL_13:
			this.ᜄ.Type = FieldType.FieldLink;
		}

		// Token: 0x06004381 RID: 17281 RVA: 0x003F45E4 File Offset: 0x003F35E4
		private void ᜂ()
		{
			int a_ = 18;
			if (true)
			{
			}
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					try
					{
						this.ᜋ = new sprḴ(this.m_doc.ObjectPool, ClipboardData.b("❷", a_) + this.OleStorageName);
						return;
					}
					catch
					{
						return;
					}
					goto IL_A5;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_A5;
					}
					if (false)
					{
					}
					goto IL_A5;
				case 2:
					if (this.m_doc.ObjectPool.Length > 0)
					{
						num = 0;
						continue;
					}
					return;
				}
				if (this.m_doc.ObjectPool != null)
				{
					num = 1;
					continue;
				}
				break;
				IL_A5:
				num = 2;
			}
		}

		// Token: 0x06004382 RID: 17282 RVA: 0x003F46D0 File Offset: 0x003F36D0
		private new byte[] ᜁ(string A_0)
		{
			int a_ = 7;
			switch (0)
			{
			default:
			{
				int num = 11;
				for (;;)
				{
					MemoryStream memoryStream;
					sprᤘ sprᤘ;
					sprᤘ sprᤘ2;
					switch (num)
					{
					case 0:
						goto IL_15B;
					case 1:
						if (this.m_doc.ObjectPool != null)
						{
							num = 12;
							continue;
						}
						goto IL_BC;
					case 2:
						this.ᜀ(A_0);
						num = 13;
						continue;
					case 3:
						goto IL_15B;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_27C;
						default:
							if (false)
							{
							}
							memoryStream.Close();
							memoryStream.Dispose();
							num = 8;
							continue;
						}
						break;
					case 5:
						if (this.m_doc.ObjectPool.Length == 0)
						{
							num = 10;
							continue;
						}
						memoryStream = new MemoryStream(this.m_doc.ObjectPool);
						sprᤘ = new sprᤘ(memoryStream, STGM.STGM_READWRITE | STGM.STGM_SHARE_EXCLUSIVE);
						sprᤘ2 = sprᤘ.ᜀ(ClipboardData.b("≬൮᭰ᙲᙴͶ⥸ᑺቼ፾", a_), STGM.STGM_READWRITE | STGM.STGM_SHARE_EXCLUSIVE);
						num = 3;
						continue;
					case 6:
						num = 9;
						continue;
					case 7:
						if (memoryStream != null)
						{
							num = 4;
							continue;
						}
						goto IL_307;
					case 8:
						goto IL_131;
					case 9:
						if (this.OleObjectType == OleObjectType.Undefined)
						{
							num = 2;
							continue;
						}
						goto IL_2D5;
					case 10:
						goto IL_BC;
					case 12:
						num = 5;
						continue;
					case 13:
						goto IL_F8;
					}
					if (this.ᜑ != null)
					{
						num = 6;
						continue;
					}
					goto IL_2D5;
					IL_BC:
					sprᤘ = sprᤘ.ᜆ();
					sprᤘ2 = sprᤘ.ᜈ(ClipboardData.b("≬൮᭰ᙲᙴͶ⥸ᑺቼ፾", a_));
					num = 0;
					continue;
					IL_27C:
					num = 7;
					continue;
					IL_15B:
					if (true)
					{
					}
					spr\u20BF spr_u20BF = new spr\u20BF(ClipboardData.b("㹬᭮ṰŲᑴၶᱸ", a_), true);
					spr_u20BF.ᜇ().ᜄ(A_0);
					spr_u20BF.\u170D().ᜁ()[1].ᜀ(spr\u20F5.ᜀ(this.OleObjectType));
					MemoryStream memoryStream2 = new MemoryStream();
					spr_u20BF.ᜆ();
					spr_u20BF.ᜂ(memoryStream2);
					spr_u20BF.ᜊ();
					memoryStream2.Flush();
					byte[] buffer = memoryStream2.ToArray();
					memoryStream2.Close();
					memoryStream2 = new MemoryStream(buffer);
					MemoryStream memoryStream3 = new MemoryStream();
					sprᤘ sprᤘ3 = new sprᤘ(memoryStream2);
					sprᤘ sprᤘ4 = sprᤘ3.ᜆ(A_0);
					sprᤘ.ᜀ(sprᤘ4, sprᤘ2);
					sprᤘ.Flush();
					sprᤘ.ᜀ(memoryStream3);
					memoryStream3.Position = 0L;
					this.m_doc.ObjectPool = memoryStream3.ToArray();
					sprᤘ3.Close();
					sprᤘ3.Dispose();
					sprᤘ4.Close();
					sprᤘ4.Dispose();
					sprᤘ.Close();
					sprᤘ.Dispose();
					sprᤘ2.Close();
					sprᤘ2.Dispose();
					memoryStream3.Close();
					memoryStream3.Dispose();
					memoryStream2.Close();
					memoryStream2.Dispose();
					goto IL_27C;
					IL_2D5:
					memoryStream = null;
					sprᤘ = null;
					sprᤘ2 = null;
					num = 1;
				}
				IL_F8:
				IL_131:
				IL_307:
				return this.m_doc.ObjectPool;
			}
			}
		}

		// Token: 0x06004383 RID: 17283 RVA: 0x003F49F0 File Offset: 0x003F39F0
		private new void ᜀ(string A_0)
		{
			int a_ = 7;
			switch (0)
			{
			default:
			{
				int num = 5;
				spr\u20BF spr_u20BF2;
				spr\u2547 spr_u;
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
							if (false)
							{
							}
							num = 4;
							continue;
						}
						break;
					case 1:
						goto IL_E6;
					case 2:
						goto IL_E1;
					case 3:
						goto IL_112;
					case 4:
					{
						if (this.m_doc.ObjectPool.Length == 0)
						{
							num = 1;
							continue;
						}
						MemoryStream a_2 = new MemoryStream(this.m_doc.ObjectPool);
						spr\u20BF spr_u20BF = new spr\u20BF(a_2);
						spr\u2547 a_3 = spr_u20BF.ᜇ().ᜅ(ClipboardData.b("≬൮᭰ᙲᙴͶ⥸ᑺቼ፾", a_));
						spr_u20BF2 = new spr\u20BF();
						spr_u20BF2.ᜇ().ᜀ(a_3);
						this.ᜀ(spr_u20BF, spr_u20BF2);
						spr_u20BF.ᜊ();
						spr_u = spr_u20BF2.ᜇ().ᜅ(ClipboardData.b("≬൮᭰ᙲᙴͶ⥸ᑺቼ፾", a_));
						num = 2;
						continue;
					}
					}
					if (this.m_doc.ObjectPool != null)
					{
						if (true)
						{
						}
						num = 0;
						continue;
					}
					IL_E6:
					spr_u20BF2 = new spr\u20BF();
					spr_u = spr_u20BF2.ᜇ().ᜄ(ClipboardData.b("≬൮᭰ᙲᙴͶ⥸ᑺቼ፾", a_));
					num = 3;
				}
				IL_E1:
				IL_112:
				spr_u = spr_u.ᜄ(A_0);
				this.ᜀ(spr_u20BF2, A_0);
				spr_u20BF2.ᜆ();
				this.m_doc.ObjectPool = (spr_u20BF2.ᜉ() as MemoryStream).ToArray();
				spr_u20BF2.ᜊ();
				return;
			}
			}
		}

		// Token: 0x06004384 RID: 17284 RVA: 0x003F4B9C File Offset: 0x003F3B9C
		private new void ᜁ()
		{
			int a_ = 17;
			switch (0)
			{
			default:
				for (;;)
				{
					int num = 3;
					for (;;)
					{
						MemoryStream memoryStream;
						switch (num)
						{
						case 0:
							goto IL_33D;
						case 1:
							num = 4;
							continue;
						case 2:
							try
							{
								spr\u20BF spr_u20BF = new spr\u20BF(memoryStream);
								try
								{
									num = 4;
									for (;;)
									{
										switch (num)
										{
										case 0:
											if (Array.IndexOf<string>(spr_u20BF.ᜇ().ᜅ(ClipboardData.b("㡶᭸ᅺ᡼᱾펂", a_)).ᜂ(), ClipboardData.b("⡶", a_) + this.ᜅ) != -1)
											{
												num = 5;
												continue;
											}
											goto IL_2AE;
										case 1:
											num = 0;
											continue;
										case 2:
											try
											{
												byte[] array = this.ᜀ();
												spr\u2578 spr_u;
												spr_u.Write(array, 0, array.Length);
												spr_u.Flush();
												spr\u20BF spr_u20BF2 = new spr\u20BF();
												spr_u20BF2.ᜇ().ᜀ(spr_u20BF.ᜇ().ᜅ(ClipboardData.b("㡶᭸ᅺ᡼᱾펂", a_)));
												this.ᜀ(spr_u20BF, spr_u20BF2);
												spr_u20BF.ᜊ();
												spr_u20BF2.ᜆ();
												this.m_doc.ObjectPool = (spr_u20BF2.ᜉ() as MemoryStream).ToArray();
												spr_u20BF2.ᜊ();
												goto IL_2AE;
											}
											finally
											{
												num = 0;
												for (;;)
												{
													spr\u2578 spr_u;
													switch (num)
													{
													case 1:
														((IDisposable)spr_u).Dispose();
														num = 2;
														continue;
													case 2:
														goto IL_239;
													}
													if (spr_u == null)
													{
														break;
													}
													num = 1;
												}
												IL_239:;
											}
											goto IL_23C;
										case 3:
										{
											spr\u2578 spr_u = spr_u20BF.ᜇ().ᜅ(ClipboardData.b("㡶᭸ᅺ᡼᱾펂", a_)).ᜅ(ClipboardData.b("⡶", a_) + this.ᜅ).ᜁ(ClipboardData.b("瑶㙸᥺᝼㙾", a_));
											num = 2;
											continue;
										}
										case 5:
											goto IL_23C;
										case 6:
											goto IL_2BA;
										case 7:
											if (Array.IndexOf<string>(spr_u20BF.ᜇ().ᜅ(ClipboardData.b("㡶᭸ᅺ᡼᱾펂", a_)).ᜅ(ClipboardData.b("⡶", a_) + this.ᜅ).ᜁ(), ClipboardData.b("瑶㙸᥺᝼㙾", a_)) != -1)
											{
												num = 3;
												continue;
											}
											goto IL_2AE;
										}
										if (Array.IndexOf<string>(spr_u20BF.ᜇ().ᜂ(), ClipboardData.b("㡶᭸ᅺ᡼᱾펂", a_)) != -1)
										{
											num = 1;
											continue;
										}
										goto IL_2AE;
										IL_23C:
										num = 7;
										continue;
										IL_2AE:
										num = 6;
									}
									IL_2BA:;
								}
								finally
								{
									num = 2;
									for (;;)
									{
										switch (num)
										{
										case 0:
											((IDisposable)spr_u20BF).Dispose();
											num = 1;
											continue;
										case 1:
											goto IL_2F7;
										}
										if (spr_u20BF == null)
										{
											break;
										}
										num = 0;
									}
									IL_2F7:;
								}
								return;
							}
							finally
							{
								num = 2;
								for (;;)
								{
									switch (num)
									{
									case 0:
										goto IL_33A;
									case 1:
										((IDisposable)memoryStream).Dispose();
										num = 0;
										continue;
									}
									if (memoryStream == null)
									{
										break;
									}
									num = 1;
								}
								IL_33A:;
							}
							goto IL_33D;
						case 3:
							if (this.m_doc.ObjectPool != null)
							{
								num = 1;
								continue;
							}
							return;
						case 4:
							IL_399:
							if (this.m_doc.ObjectPool.Length != 0)
							{
								num = 0;
								continue;
							}
							return;
						}
						break;
						IL_33D:
						if (true)
						{
						}
						memoryStream = new MemoryStream(this.m_doc.ObjectPool);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_399;
						default:
							if (false)
							{
							}
							num = 2;
							break;
						}
					}
				}
				return;
			}
		}

		// Token: 0x06004385 RID: 17285 RVA: 0x003F4FB0 File Offset: 0x003F3FB0
		private new byte[] ᜀ()
		{
			switch (0)
			{
			default:
			{
				byte[] result;
				for (;;)
				{
					result = new byte[6];
					OleObjectType oleObjectType = this.OleObjectType;
					int num = 18;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							byte[] array = new byte[6];
							array[2] = 3;
							array[4] = 1;
							result = array;
							num = 5;
							continue;
						}
						case 1:
							if (this.LinkType == OleLinkType.Embed)
							{
								num = 25;
								continue;
							}
							return result;
						case 2:
						{
							byte[] array2 = new byte[6];
							array2[2] = 3;
							array2[4] = 4;
							result = array2;
							num = 26;
							continue;
						}
						case 3:
							goto IL_2FF;
						case 4:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_43F;
							default:
								goto IL_3EC;
							}
							break;
						case 5:
							goto IL_205;
						case 6:
							goto IL_175;
						case 7:
							goto IL_43F;
						case 8:
							if (this.LinkType == OleLinkType.Embed)
							{
								if (true)
								{
								}
								num = 20;
								continue;
							}
							result = new byte[]
							{
								16,
								0,
								3,
								0,
								13,
								0
							};
							num = 3;
							continue;
						case 9:
							if (this.LinkType == OleLinkType.Embed)
							{
								num = 7;
								continue;
							}
							result = new byte[]
							{
								16,
								0,
								3,
								0,
								13,
								0
							};
							num = 14;
							continue;
						case 10:
							if (this.LinkType == OleLinkType.Embed)
							{
								num = 2;
								continue;
							}
							result = new byte[]
							{
								16,
								2,
								3,
								0,
								13,
								0
							};
							num = 30;
							continue;
						case 11:
							result = new byte[]
							{
								64,
								0,
								3,
								0,
								4,
								0
							};
							num = 15;
							continue;
						case 12:
							goto IL_380;
						case 13:
							if (this.LinkType == OleLinkType.Embed)
							{
								num = 24;
								continue;
							}
							result = new byte[]
							{
								16,
								2,
								3,
								0,
								13,
								0
							};
							num = 17;
							continue;
						case 14:
							goto IL_198;
						case 15:
							return result;
						case 16:
							if (this.LinkType == OleLinkType.Embed)
							{
								num = 27;
								continue;
							}
							result = new byte[]
							{
								16,
								0,
								3,
								0,
								4,
								0
							};
							num = 6;
							continue;
						case 17:
							goto IL_2DC;
						case 18:
							switch (oleObjectType)
							{
							case OleObjectType.Undefined:
							case OleObjectType.WordPadDocument:
								num = 10;
								continue;
							case OleObjectType.AdobeAcrobatDocument:
							case OleObjectType.Excel_97_2003_Worksheet:
							case OleObjectType.ExcelBinaryWorksheet:
							case OleObjectType.ExcelMacroWorksheet:
							case OleObjectType.ExcelWorksheet:
							case OleObjectType.PowerPoint_97_2003_Presentation:
							case OleObjectType.PowerPointMacroPresentation:
							case OleObjectType.PowerPointMacroSlide:
							case OleObjectType.PowerPointPresentation:
							case OleObjectType.PowerPointSlide:
							case OleObjectType.WordMacroDocument:
							case OleObjectType.VisioDrawing:
							case OleObjectType.OpenDocumentPresentation:
							case OleObjectType.OpenDocumentSpreadsheet:
							case OleObjectType.OpenOfficeSpreadsheet1_1:
							case OleObjectType.OpenOfficeText_1_1:
							case OleObjectType.OpenOfficeSpreadsheet:
							case OleObjectType.OpenOfficeText:
								num = 9;
								continue;
							case OleObjectType.BitmapImage:
							case OleObjectType.MIDISequence:
							case OleObjectType.VideoClip:
								num = 16;
								continue;
							case OleObjectType.MediaClip:
							case OleObjectType.Package:
							case OleObjectType.WaveSound:
								result = new byte[]
								{
									64,
									0,
									3,
									0,
									4,
									0
								};
								num = 23;
								continue;
							case OleObjectType.Equation:
								num = 1;
								continue;
							case OleObjectType.GraphChart:
							case OleObjectType.ExcelChart:
								num = 8;
								continue;
							case OleObjectType.PowerPoint_97_2003_Slide:
							case OleObjectType.WordDocument:
								num = 32;
								continue;
							case OleObjectType.Word_97_2003_Document:
								num = 13;
								continue;
							case OleObjectType.OpenDocumentText:
								return result;
							default:
								num = 21;
								continue;
							}
							break;
						case 19:
							goto IL_310;
						case 20:
							result = new byte[]
							{
								0,
								2,
								3,
								0,
								13,
								0
							};
							num = 12;
							continue;
						case 21:
							num = 19;
							continue;
						case 22:
						{
							if (this.ᜐ)
							{
								num = 11;
								continue;
							}
							byte[] array3 = new byte[6];
							array3[2] = 3;
							array3[4] = 13;
							result = array3;
							num = 33;
							continue;
						}
						case 23:
							goto IL_228;
						case 24:
							result = new byte[]
							{
								0,
								2,
								3,
								0,
								1,
								0
							};
							num = 31;
							continue;
						case 25:
						{
							byte[] array4 = new byte[6];
							array4[2] = 3;
							array4[4] = 4;
							result = array4;
							num = 28;
							continue;
						}
						case 26:
							goto IL_3A6;
						case 27:
						{
							byte[] array5 = new byte[6];
							array5[2] = 3;
							array5[4] = 4;
							result = array5;
							num = 4;
							continue;
						}
						case 28:
							goto IL_294;
						case 29:
							goto IL_35D;
						case 30:
							goto IL_1E3;
						case 31:
							goto IL_24B;
						case 32:
							if (this.LinkType == OleLinkType.Embed)
							{
								num = 0;
								continue;
							}
							result = new byte[]
							{
								16,
								0,
								3,
								0,
								13,
								0
							};
							num = 29;
							continue;
						case 33:
							goto IL_272;
						}
						break;
						IL_43F:
						num = 22;
					}
				}
				IL_175:
				IL_198:
				IL_1E3:
				IL_205:
				IL_228:
				IL_24B:
				IL_272:
				IL_294:
				IL_2DC:
				IL_2FF:
				IL_310:
				IL_35D:
				IL_380:
				IL_3A6:
				return result;
				IL_3EC:
				if (false)
				{
				}
				return result;
			}
			}
		}

		// Token: 0x06004386 RID: 17286 RVA: 0x003F549C File Offset: 0x003F449C
		private new void ᜀ(spr\u20BF A_0, spr\u20BF A_1)
		{
			int a_ = 14;
			switch (0)
			{
			default:
				if (true)
				{
				}
				this.\u1712.Clear();
				using (List<spr\u2486>.Enumerator enumerator = A_0.\u170D().ᜁ().GetEnumerator())
				{
					int num = 8;
					for (;;)
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
							switch (num)
							{
							case 0:
								num = 7;
								continue;
							case 1:
							{
								if (!enumerator.MoveNext())
								{
									num = 0;
									continue;
								}
								spr\u2486 spr_u = enumerator.Current;
								goto IL_220;
							}
							case 3:
								num = 4;
								continue;
							case 4:
							{
								spr\u2486 spr_u;
								if (this.\u1712.ContainsKey(spr_u.ᜀ()))
								{
									num = 6;
									continue;
								}
								this.\u1712.Add(spr_u.ᜀ(), spr_u.ᜇ());
								num = 2;
								continue;
							}
							case 5:
							{
								spr\u2486 spr_u;
								if (spr_u.ᜄ() == spr\u2486.EntryType.Storage)
								{
									num = 3;
									continue;
								}
								break;
							}
							case 6:
							{
								spr\u2486 spr_u;
								this.\u1712[spr_u.ᜀ()] = spr_u.ᜇ();
								num = 10;
								continue;
							}
							case 7:
								goto IL_28F;
							case 9:
							{
								spr\u2486 spr_u;
								if (!(spr_u.ᜀ() == ClipboardData.b("㭳ᑵቷόύ੽큿", a_)))
								{
									num = 11;
									continue;
								}
								break;
							}
							case 11:
								num = 5;
								continue;
							}
							IL_1A3:
							num = 1;
							continue;
							goto IL_1A3;
						}
						IL_220:
						num = 9;
					}
					IL_28F:
					goto IL_10A;
				}
				return;
				for (;;)
				{
					IL_10A:
					A_0.ᜊ();
					using (List<spr\u2486>.Enumerator enumerator2 = A_1.\u170D().ᜁ().GetEnumerator())
					{
						int num = 4;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_F7;
							case 2:
								num = 0;
								continue;
							case 3:
							{
								if (!enumerator2.MoveNext())
								{
									num = 2;
									continue;
								}
								spr\u2486 spr_u2 = enumerator2.Current;
								num = 5;
								continue;
							}
							case 5:
							{
								spr\u2486 spr_u2;
								if (this.\u1712.ContainsKey(spr_u2.ᜀ()))
								{
									num = 6;
									continue;
								}
								break;
							}
							case 6:
							{
								spr\u2486 spr_u2;
								spr_u2.ᜀ(this.\u1712[spr_u2.ᜀ()]);
								num = 1;
								continue;
							}
							}
							IL_7A:
							num = 3;
							continue;
							goto IL_7A;
						}
						IL_F7:
						break;
					}
				}
				return;
			}
		}

		// Token: 0x06004387 RID: 17287 RVA: 0x003F5780 File Offset: 0x003F4780
		private new void ᜀ(spr\u20BF A_0, string A_1)
		{
			switch (0)
			{
			default:
			{
				spr\u20BF spr_u20BF = new spr\u20BF(this.ᜑ);
				this.ᜑ.Position = 0L;
				Guid guid = spr\u20F5.ᜀ(this.OleObjectType);
				using (List<spr\u2486>.Enumerator enumerator = spr_u20BF.\u170D().ᜁ().GetEnumerator())
				{
					int num = 5;
					for (;;)
					{
						spr\u2486 spr_u;
						switch (num)
						{
						case 0:
							goto IL_1E3;
						case 1:
							goto IL_1EF;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_161;
							default:
								if (false)
								{
								}
								guid = spr_u.ᜇ();
								num = 3;
								continue;
							}
							break;
						case 3:
							goto IL_1E3;
						case 4:
							if (!enumerator.MoveNext())
							{
								num = 0;
								continue;
							}
							goto IL_161;
						case 6:
							if (spr_u20BF.ᜇ().ᜃ() == spr_u.ᜀ())
							{
								num = 2;
								continue;
							}
							break;
						}
						goto IL_15F;
						IL_161:
						spr_u = enumerator.Current;
						num = 6;
						continue;
						IL_195:
						num = 4;
						continue;
						IL_15F:
						goto IL_195;
						IL_1E3:
						num = 1;
					}
					IL_1EF:
					goto IL_10A;
				}
				return;
				for (;;)
				{
					IL_10A:
					spr_u20BF.ᜊ();
					List<spr\u2486>.Enumerator enumerator2 = A_0.\u170D().ᜁ().GetEnumerator();
					try
					{
						int num = 5;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_EF;
							case 1:
							{
								spr\u2486 spr_u2;
								if (A_1 == spr_u2.ᜀ())
								{
									num = 6;
									continue;
								}
								break;
							}
							case 2:
								goto IL_E3;
							case 3:
								goto IL_E3;
							case 4:
							{
								if (!enumerator2.MoveNext())
								{
									num = 3;
									continue;
								}
								spr\u2486 spr_u2 = enumerator2.Current;
								num = 1;
								continue;
							}
							case 6:
							{
								spr\u2486 spr_u2;
								spr_u2.ᜀ(guid);
								this.\u1712.Add(A_1, guid);
								num = 2;
								continue;
							}
							}
							IL_7A:
							num = 4;
							continue;
							goto IL_7A;
							IL_E3:
							num = 0;
						}
						IL_EF:
						break;
					}
					finally
					{
						if (true)
						{
						}
						((IDisposable)enumerator2).Dispose();
					}
				}
				return;
			}
			}
		}

		// Token: 0x06004388 RID: 17288 RVA: 0x003F59AC File Offset: 0x003F49AC
		internal void ᜂ(string A_0)
		{
			int a_ = 4;
			for (;;)
			{
				for (;;)
				{
					this.ᜅ = DocOleObject.NextOleObjId.ToString();
					byte[] a_2 = this.ᜁ(ClipboardData.b("㕩", a_) + this.ᜅ);
					this.ᜋ = new sprḴ();
					this.m_doc.ObjectPool = this.ᜋ.ᜀ(a_2, A_0, ClipboardData.b("㕩", a_) + this.ᜅ, this.ᜉ, this.ᜈ);
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							this.ᜁ();
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								num = 1;
								continue;
							}
							break;
						case 1:
							return;
						case 2:
							if (true)
							{
							}
							if (!this.ᜐ)
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
		}

		// Token: 0x06004389 RID: 17289 RVA: 0x003F5AB4 File Offset: 0x003F4AB4
		internal new void ᜀ(byte[] A_0, string A_1)
		{
			int a_ = 17;
			int num = 1;
			for (;;)
			{
				string text;
				switch (num)
				{
				case 0:
					text = A_1;
					goto IL_63;
				case 2:
					this.ᜁ();
					num = 6;
					continue;
				case 3:
					if (this.ᜐ)
					{
						return;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3B;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 4:
					num = 0;
					continue;
				case 5:
					text = string.Empty;
					goto IL_63;
				case 6:
					return;
				}
				goto IL_35;
				IL_3B:
				num = 4;
				continue;
				IL_35:
				if (A_1 != null)
				{
					goto IL_3B;
				}
				if (true)
				{
				}
				num = 5;
				continue;
				IL_63:
				A_1 = text;
				this.ᜅ = DocOleObject.NextOleObjId.ToString();
				byte[] a_2 = this.ᜁ(ClipboardData.b("⡶", a_) + this.ᜅ);
				this.ᜋ = new sprḴ();
				this.ᜋ.ᜀ(this.ᜎ, this.ᜏ);
				this.m_doc.ObjectPool = this.ᜋ.ᜀ(a_2, A_0, A_1, this);
				this.OleObjectType = this.ᜋ.ᜂ();
				num = 3;
			}
		}

		// Token: 0x0600438A RID: 17290 RVA: 0x003F5C0C File Offset: 0x003F4C0C
		internal void ᜃ(string A_0)
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

		// Token: 0x0600438B RID: 17291 RVA: 0x003F5C48 File Offset: 0x003F4C48
		internal new void ᜀ(string A_0, string A_1)
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
			this.ᜎ = A_0;
			this.ᜏ = A_1;
		}

		// Token: 0x0600438C RID: 17292 RVA: 0x003F5C94 File Offset: 0x003F4C94
		protected override void CreateLayoutInfo()
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
			this.ᜀ = new spr\u22A8(ChildrenLayoutDirection.Horizontal);
			this.ᜀ.ᜀ(true);
		}

		// Token: 0x0600438D RID: 17293 RVA: 0x003F5CE8 File Offset: 0x003F4CE8
		protected override object CloneImpl()
		{
			DocOleObject docOleObject;
			for (;;)
			{
				docOleObject = (DocOleObject)base.CloneImpl();
				int num = 10;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						byte[] array = new byte[this.ᜃ.Length];
						this.ᜃ.Read(array, 0, array.Length);
						this.ᜃ.Position = 0L;
						MemoryStream a_ = new MemoryStream(array);
						docOleObject.ᜀ(a_);
						num = 4;
						continue;
					}
					case 1:
						goto IL_140;
					case 2:
						if (this.Container != null)
						{
							num = 0;
							continue;
						}
						goto IL_84;
					case 3:
						goto IL_F7;
					case 4:
						goto IL_84;
					case 5:
						docOleObject.ᜀ(this.ᜂ.Clone() as DocPicture);
						docOleObject.OlePicture.ᜀ(docOleObject);
						num = 9;
						continue;
					case 6:
						if (this.ᜊ == null)
						{
							goto IL_1CD;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_17A;
						default:
							if (false)
							{
							}
							num = 8;
							continue;
						}
						break;
					case 7:
						if (this.ᜄ != null)
						{
							num = 11;
							continue;
						}
						goto IL_140;
					case 8:
						goto IL_17A;
					case 9:
						goto IL_B3;
					case 10:
						if (this.ᜂ != null)
						{
							num = 5;
							continue;
						}
						goto IL_B3;
					case 11:
						docOleObject.Field = (this.ᜄ.Clone() as Field);
						num = 1;
						continue;
					}
					break;
					IL_84:
					docOleObject.ᜈ = this.OleObjectType;
					num = 7;
					continue;
					IL_B3:
					num = 2;
					continue;
					IL_140:
					num = 6;
					continue;
					IL_17A:
					docOleObject.OleXmlItem = (this.ᜊ.Clone() as spr\u24D5);
					num = 3;
				}
			}
			IL_F7:
			if (true)
			{
			}
			IL_1CD:
			docOleObject.ᜁ = true;
			return docOleObject;
		}

		// Token: 0x0600438E RID: 17294 RVA: 0x003F5ECC File Offset: 0x003F4ECC
		internal override void CloneRelationsTo(Document doc, OwnerHolder nextOwner)
		{
			for (;;)
			{
				base.CloneRelationsTo(doc, nextOwner);
				int a_ = int.Parse(this.OleStorageName);
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.Container != null)
						{
							num = 1;
							continue;
						}
						goto IL_73;
					case 1:
						if (true)
						{
						}
						spr\u1C2D.ᜀ(this.Container, a_, doc);
						this.ᜃ.Close();
						this.ᜃ = null;
						num = 2;
						continue;
					case 2:
						goto IL_71;
					}
					break;
				}
			}
			IL_71:
			IL_73:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_71;
			default:
				if (false)
				{
				}
				this.ᜁ = false;
				return;
			}
		}

		// Token: 0x0600438F RID: 17295 RVA: 0x003F5F7C File Offset: 0x003F4F7C
		internal override void Close()
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
						break;
					default:
						goto IL_66;
					}
					break;
				case 1:
					this.ᜂ.Close();
					this.ᜂ = null;
					num = 0;
					continue;
				}
				if (this.ᜂ == null)
				{
					goto IL_78;
				}
				if (true)
				{
				}
				num = 1;
			}
			IL_66:
			if (false)
			{
			}
			IL_78:
			this.\u170D = null;
			this.ᜄ = null;
			this.ᜊ = null;
		}

		// Token: 0x06004390 RID: 17296 RVA: 0x003F6018 File Offset: 0x003F5018
		SizeF spr\u2297.Measure(spr\u19E0 dc)
		{
			int num = 0;
			for (;;)
			{
				DocumentObject documentObject;
				switch (num)
				{
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_199;
					default:
						if (false)
						{
						}
						num = 12;
						continue;
					}
					break;
				case 2:
					this.ᜂ.Height = this.ᜂ.Height * this.ᜂ.HeightScale / 100f;
					this.ᜂ.HeightScale = 100f;
					num = 16;
					continue;
				case 3:
					goto IL_1DF;
				case 4:
					if ((documentObject.NextSibling as FieldMark).Type == FieldMarkType.FieldEnd)
					{
						num = 18;
						continue;
					}
					goto IL_10E;
				case 5:
					if (documentObject.NextSibling is FieldMark)
					{
						num = 17;
						continue;
					}
					goto IL_10E;
				case 6:
					if (this.ᜂ.WidthScale != 100f)
					{
						num = 7;
						continue;
					}
					goto IL_1D3;
				case 7:
					this.ᜂ.Width = this.ᜂ.Width * this.ᜂ.WidthScale / 100f;
					this.ᜂ.WidthScale = 100f;
					num = 9;
					continue;
				case 8:
					if (documentObject is DocPicture)
					{
						if (true)
						{
						}
						num = 13;
						continue;
					}
					goto IL_1DF;
				case 9:
					goto IL_18B;
				case 10:
					if (this.ᜂ != null)
					{
						num = 19;
						continue;
					}
					goto IL_2C6;
				case 11:
					if (this.ᜂ.HeightScale != 100f)
					{
						num = 2;
						continue;
					}
					goto IL_237;
				case 12:
					if ((base.NextSibling as FieldMark).Type == FieldMarkType.FieldSeparator)
					{
						num = 15;
						continue;
					}
					goto IL_E8;
				case 13:
				{
					DocPicture docPicture = documentObject as DocPicture;
					docPicture.ᜀ = new spr\u22A8();
					docPicture.ᜀ.ᜁ(true);
					num = 3;
					continue;
				}
				case 14:
					goto IL_1DF;
				case 15:
					documentObject = (base.NextSibling as DocumentObject);
					goto IL_199;
				case 16:
					goto IL_237;
				case 17:
					num = 4;
					continue;
				case 18:
					goto IL_E8;
				case 19:
					num = 11;
					continue;
				}
				if (base.NextSibling is FieldMark)
				{
					num = 1;
					continue;
				}
				IL_E8:
				num = 10;
				continue;
				IL_10E:
				documentObject = (documentObject.NextSibling as DocumentObject);
				num = 8;
				continue;
				IL_199:
				num = 14;
				continue;
				IL_1DF:
				num = 5;
				continue;
				IL_237:
				num = 6;
			}
			IL_18B:
			IL_1D3:
			return this.ᜂ.Size;
			IL_2C6:
			return SizeF.Empty;
		}

		// Token: 0x06004391 RID: 17297 RVA: 0x003F62F0 File Offset: 0x003F52F0
		void spr\u1AB8.Draw(spr\u19E0 dc, sprᦰ ltWidget)
		{
			int num = 1;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					dc.ᜁ(this.ᜂ, ltWidget, true);
					num = 2;
					continue;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_62;
					}
					break;
				}
				if (this.ᜂ == null)
				{
					return;
				}
				num = 0;
			}
			IL_62:
			if (false)
			{
			}
		}

		// Token: 0x04003585 RID: 13701
		private new const string ᜀ = "ObjectPool";

		// Token: 0x04003586 RID: 13702
		private new const int ᜁ = 6;

		// Token: 0x04003587 RID: 13703
		internal DocPicture ᜂ;

		// Token: 0x04003588 RID: 13704
		private Stream ᜃ;

		// Token: 0x04003589 RID: 13705
		private int \u2609\u0094\u009C\u00A3;

		// Token: 0x0400358A RID: 13706
		private new Field ᜄ;

		// Token: 0x0400358B RID: 13707
		private string ᜅ;

		// Token: 0x0400358C RID: 13708
		private string ᜆ;

		// Token: 0x0400358D RID: 13709
		private string ᜇ;

		// Token: 0x0400358E RID: 13710
		private OleObjectType ᜈ;

		// Token: 0x0400358F RID: 13711
		internal OleLinkType ᜉ;

		// Token: 0x04003590 RID: 13712
		private spr\u24D5 ᜊ;

		// Token: 0x04003591 RID: 13713
		private sprḴ ᜋ;

		// Token: 0x04003592 RID: 13714
		private static Random ᜌ;

		// Token: 0x04003593 RID: 13715
		private byte[] \u170D;

		// Token: 0x04003594 RID: 13716
		private string ᜎ = string.Empty;

		// Token: 0x04003595 RID: 13717
		private string ᜏ = string.Empty;

		// Token: 0x04003596 RID: 13718
		private bool ᜐ = true;

		// Token: 0x04003597 RID: 13719
		internal Stream ᜑ;

		// Token: 0x04003598 RID: 13720
		internal new Dictionary<string, Guid> \u1712 = new Dictionary<string, Guid>();
	}
}
