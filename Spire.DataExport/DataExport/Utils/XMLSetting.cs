using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using Spire.DataExport.CollectionEditors;

namespace Spire.DataExport.Utils
{
	// Token: 0x0200023B RID: 571
	public class XMLSetting
	{
		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06001152 RID: 4434 RVA: 0x000BAB40 File Offset: 0x000B9B40
		public string XmlComments
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
				return this.XmlComments;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06001153 RID: 4435 RVA: 0x000BAB84 File Offset: 0x000B9B84
		public bool Dirty
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
				return this.ᜄ;
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06001154 RID: 4436 RVA: 0x000BABC8 File Offset: 0x000B9BC8
		// (set) Token: 0x06001155 RID: 4437 RVA: 0x000BAC0C File Offset: 0x000B9C0C
		public IniSections Sections
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
				return this.ᜀ;
			}
			set
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
				this.ᜀ = value;
			}
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x000BAC50 File Offset: 0x000B9C50
		public XMLSetting()
		{
			this.ᜀ = new IniSections(this);
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x000BAC70 File Offset: 0x000B9C70
		public XMLSetting(string fileName)
		{
			try
			{
				this.ᜀ = new IniSections(this);
				this.ᜂ = this.ᜀ();
				this.ᜁ = this.ᜀ(fileName);
			}
			catch
			{
				throw;
			}
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x000BACC0 File Offset: 0x000B9CC0
		public void Remove(string sectionName, string settingName)
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
			this.Sections[sectionName].Settings.Remove(settingName);
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x000BAD14 File Offset: 0x000B9D14
		public void Remove(string sectionName)
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
			this.Sections.Remove(sectionName);
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x000BAD5C File Offset: 0x000B9D5C
		public IniSetting SetVal(string sectionName, string settingName, object oValue)
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
			IniSetting iniSetting = this.Sections.Add(sectionName).Settings.Add(settingName);
			iniSetting.Value = oValue;
			return iniSetting;
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x000BADB8 File Offset: 0x000B9DB8
		public IniSetting SetVal(string sectionName, string settingName, object oValue, string description)
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
			IniSetting iniSetting = this.SetVal(sectionName, settingName, oValue);
			iniSetting.Description = description;
			return iniSetting;
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x000BAE08 File Offset: 0x000B9E08
		public IniSetting GetVal(string sectionName, string settingName)
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
			return this.Sections[sectionName].Settings[settingName];
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x000BAE5C File Offset: 0x000B9E5C
		public IniSetting GetVal(string sectionName, string settingName, object oDefault)
		{
			IniSetting iniSetting;
			for (;;)
			{
				IniSection iniSection = this.ᜀ.Add(sectionName);
				iniSetting = iniSection.Settings[settingName];
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						iniSetting = iniSection.Settings.Add(settingName);
						iniSetting.ᜉ = this;
						iniSetting.Value = oDefault;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return iniSetting;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							num = 1;
							continue;
						}
						break;
					case 1:
						return iniSetting;
					case 2:
						if (iniSetting == null)
						{
							num = 0;
							continue;
						}
						return iniSetting;
					}
					break;
				}
			}
			return iniSetting;
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x0600115E RID: 4446 RVA: 0x000BAF04 File Offset: 0x000B9F04
		// (set) Token: 0x0600115F RID: 4447 RVA: 0x000BAF48 File Offset: 0x000B9F48
		public string PathToXmlFile
		{
			get
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
				return this.ᜆ;
			}
			set
			{
				int a_ = 8;
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_CC:
					num = 1;
					break;
				case 1:
					goto IL_29;
				default:
					goto IL_29;
				}
				for (;;)
				{
					IL_31:
					switch (num)
					{
					case 0:
						goto IL_B1;
					case 1:
						goto IL_D7;
					case 2:
						num = 0;
						continue;
					case 3:
						if (Directory.Exists(this.ᜆ))
						{
							num = 2;
							continue;
						}
						goto IL_75;
					}
					goto IL_47;
				}
				IL_75:
				throw new ApplicationException(HyperlinksCollectionEditor.b("戣䤥䐧丩䤫尭ု嘱嬳匵䬷ᨹ刻儽㐿扁⅃㹅ⅇ㥉㡋恍", a_));
				IL_B1:
				if (!this.ᜆ.EndsWith(HyperlinksCollectionEditor.b("砣", a_)))
				{
					goto IL_CC;
				}
				return;
				IL_D7:
				this.ᜆ += HyperlinksCollectionEditor.b("砣", a_);
				return;
				IL_29:
				if (false)
				{
				}
				IL_47:
				if (true)
				{
				}
				this.ᜆ = value;
				num = 3;
				goto IL_31;
			}
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x000BB030 File Offset: 0x000BA030
		public bool Save(string pathAndFileName)
		{
			bool result;
			try
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
				this.ᜁ = this.ᜀ(pathAndFileName);
				result = this.Save();
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x000BB098 File Offset: 0x000BA098
		public bool Save()
		{
			bool result;
			try
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				bool flag = this.ᜅ(this.ᜁ);
				this.ᜄ = flag;
				this.ᜅ = File.GetLastWriteTime(this.ᜁ);
				result = flag;
			}
			catch
			{
				result = false;
			}
			if (true)
			{
			}
			return result;
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x000BB114 File Offset: 0x000BA114
		public bool Load(string pathAndFileName)
		{
			bool result;
			try
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.ᜁ = this.ᜀ(pathAndFileName);
				result = this.Load();
			}
			catch
			{
				result = false;
			}
			if (true)
			{
			}
			return result;
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x000BB17C File Offset: 0x000BA17C
		public bool Load()
		{
			bool result;
			try
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				bool flag = this.ᜄ(this.ᜁ);
				this.ᜄ = flag;
				this.ᜅ = File.GetLastWriteTime(this.ᜁ);
				result = flag;
			}
			catch
			{
				result = false;
			}
			if (true)
			{
			}
			return result;
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06001164 RID: 4452 RVA: 0x000BB1F8 File Offset: 0x000BA1F8
		public bool FileChanged
		{
			get
			{
				bool result;
				try
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
					result = (this.ᜅ != File.GetLastWriteTime(this.ᜁ));
				}
				catch
				{
					result = true;
				}
				return result;
			}
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x000BB264 File Offset: 0x000BA264
		private bool ᜅ(string A_0)
		{
			int a_ = 5;
			switch (0)
			{
			default:
			{
				bool result = true;
				this.LastXmlError = "";
				XmlTextWriter xmlTextWriter = new XmlTextWriter(A_0, null);
				try
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
						xmlTextWriter.Formatting = Formatting.Indented;
						xmlTextWriter.WriteStartDocument(true);
						xmlTextWriter.WriteComment(HyperlinksCollectionEditor.b("爠匢䰤唦䰨Ԫ椬丮䔰刲瀴伶䤸吺似䬾慀ᭂࡄ୆楈⡊≌ⅎ㝐㩒㉔≖⭘㩚⥜㙞๠ൢ䕤Ŧhݪ࡬", a_));
						xmlTextWriter.WriteComment(HyperlinksCollectionEditor.b("礠渢椤ܦ缨个弬尮堰尲嬴ਸ਼ସᔺ഼", a_));
						xmlTextWriter.WriteStartElement(HyperlinksCollectionEditor.b("爠䘢䘤匦䀨䐪䌬尮", a_));
						IEnumerator enumerator = this.Sections.GetEnumerator();
						try
						{
							int num = 0;
							for (;;)
							{
								switch (num)
								{
								case 1:
									goto IL_151;
								case 3:
									num = 1;
									continue;
								case 4:
								{
									if (!enumerator.MoveNext())
									{
										num = 3;
										continue;
									}
									IniSection iniSection = (IniSection)enumerator.Current;
									xmlTextWriter.WriteStartElement(HyperlinksCollectionEditor.b("爠䘢䘤匦䀨䐪䌬", a_));
									xmlTextWriter.WriteAttributeString(HyperlinksCollectionEditor.b("漠䈢䠤䈦", a_), iniSection.Name);
									this.ᜀ(iniSection.Settings, ref xmlTextWriter);
									xmlTextWriter.WriteEndElement();
									num = 2;
									continue;
								}
								}
								IL_125:
								num = 4;
								continue;
								goto IL_125;
							}
							IL_151:;
						}
						finally
						{
							for (;;)
							{
								IDisposable disposable = enumerator as IDisposable;
								int num = 2;
								for (;;)
								{
									switch (num)
									{
									case 0:
										disposable.Dispose();
										num = 1;
										continue;
									case 1:
										goto IL_199;
									case 2:
										if (disposable != null)
										{
											num = 0;
											continue;
										}
										goto IL_19B;
									}
									break;
								}
							}
							IL_199:
							IL_19B:;
						}
						break;
					}
					}
					xmlTextWriter.WriteEndElement();
				}
				catch (Exception ex)
				{
					this.LastXmlError = HyperlinksCollectionEditor.b("戠䈢䬤䤦䘨弪ബ堮䌰娲䄴制ᤸ䠺堼䬾㕀⩂⭄⁆㩈敊浌", a_) + ex.Message;
					result = false;
				}
				finally
				{
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_205;
						case 2:
							xmlTextWriter.Close();
							num = 0;
							continue;
						}
						if (xmlTextWriter == null)
						{
							break;
						}
						num = 2;
					}
					IL_205:
					result = true;
				}
				if (true)
				{
				}
				return result;
			}
			}
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x000BB4DC File Offset: 0x000BA4DC
		private void ᜀ(IniSettings A_0, ref XmlTextWriter A_1)
		{
			int a_ = 13;
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			}
			if (false)
			{
			}
			switch (0)
			{
			default:
			{
				IEnumerator enumerator = A_0.GetEnumerator();
				try
				{
					int num = 31;
					for (;;)
					{
						IniSetting iniSetting;
						switch (num)
						{
						case 0:
							goto IL_322;
						case 1:
						{
							string typeName;
							if ((typeName = iniSetting.TypeName) != null)
							{
								num = 17;
								continue;
							}
							goto IL_4C0;
						}
						case 2:
							goto IL_492;
						case 3:
							num = 4;
							continue;
						case 4:
							goto IL_6DC;
						case 5:
							goto IL_492;
						case 6:
							goto IL_492;
						case 7:
							if (iniSetting != null)
							{
								num = 27;
								continue;
							}
							break;
						case 8:
							goto IL_492;
						case 9:
							goto IL_492;
						case 10:
							spr\u1DE5.ᜎ = new Dictionary<string, int>(11)
							{
								{
									HyperlinksCollectionEditor.b("怨䔪䐬簮吰䜲䄴帶圸尺丼", a_),
									0
								},
								{
									HyperlinksCollectionEditor.b("怨䔪䐬簮吰倲䄴帶嘸唺", a_),
									1
								},
								{
									HyperlinksCollectionEditor.b("栨太弬丮䠰缲尴䐶䴸", a_),
									2
								},
								{
									HyperlinksCollectionEditor.b("樨䐪䄬䀮䌰", a_),
									3
								},
								{
									HyperlinksCollectionEditor.b("漨䐪䌬嬮", a_),
									4
								},
								{
									HyperlinksCollectionEditor.b("稨䈪圬䨮", a_),
									5
								},
								{
									HyperlinksCollectionEditor.b("礨䐪䐬䄮䔰", a_),
									6
								},
								{
									HyperlinksCollectionEditor.b("洨䨪夬䨮攰娲場制", a_),
									7
								},
								{
									HyperlinksCollectionEditor.b("洨个丬䘮尰刲头", a_),
									8
								},
								{
									HyperlinksCollectionEditor.b("洨䐪堬䴮崰嘲", a_),
									9
								},
								{
									HyperlinksCollectionEditor.b("稨䈪䌬䠮崰嘲", a_),
									10
								}
							};
							num = 34;
							continue;
						case 11:
							goto IL_492;
						case 12:
							goto IL_492;
						case 13:
							num = 16;
							continue;
						case 14:
							goto IL_492;
						case 15:
							if (iniSetting.Description.Length > 0)
							{
								num = 23;
								continue;
							}
							goto IL_5CE;
						case 16:
							goto IL_4C0;
						case 17:
							num = 24;
							continue;
						case 18:
							goto IL_492;
						case 19:
						{
							int num2;
							switch (num2)
							{
							case 0:
								this.ᜀ(iniSetting, ref A_1);
								num = 2;
								continue;
							case 1:
								A_1.WriteStartElement(HyperlinksCollectionEditor.b("稨个丬嬮堰尲嬴", a_));
								A_1.WriteAttributeString(HyperlinksCollectionEditor.b("木䨪䀬䨮", a_), iniSetting.Name);
								this.ᜀ(iniSetting.Settings, ref A_1);
								A_1.WriteEndElement();
								num = 8;
								continue;
							case 2:
								this.ᜀ(iniSetting, ref A_1);
								num = 18;
								continue;
							case 3:
							{
								Color color = iniSetting;
								A_1.WriteElementString(HyperlinksCollectionEditor.b("缨䨪䄬娮吰", a_), color.ToArgb().ToString());
								num = 32;
								continue;
							}
							case 4:
								A_1.WriteElementString(HyperlinksCollectionEditor.b("缨䨪䄬娮吰", a_), this.ᜀ(iniSetting));
								num = 6;
								continue;
							case 5:
								A_1.WriteElementString(HyperlinksCollectionEditor.b("缨䨪䄬娮吰", a_), this.ᜀ(iniSetting));
								num = 33;
								continue;
							case 6:
								A_1.WriteElementString(HyperlinksCollectionEditor.b("缨䨪䄬娮吰", a_), this.ᜀ(iniSetting));
								num = 12;
								continue;
							case 7:
								A_1.WriteElementString(HyperlinksCollectionEditor.b("缨䨪䄬娮吰", a_), iniSetting.ToString(DateTimeFormatInfo.InvariantInfo));
								num = 14;
								continue;
							case 8:
								A_1.WriteElementString(HyperlinksCollectionEditor.b("缨䨪䄬娮吰", a_), iniSetting.ToString(NumberFormatInfo.InvariantInfo));
								num = 9;
								continue;
							case 9:
								A_1.WriteElementString(HyperlinksCollectionEditor.b("缨䨪䄬娮吰", a_), iniSetting.ToString(NumberFormatInfo.InvariantInfo));
								num = 28;
								continue;
							case 10:
								A_1.WriteElementString(HyperlinksCollectionEditor.b("缨䨪䄬娮吰", a_), iniSetting.ToString(NumberFormatInfo.InvariantInfo));
								num = 11;
								continue;
							default:
								num = 13;
								continue;
							}
							break;
						}
						case 20:
							if (iniSetting.Tag.Length > 0)
							{
								num = 22;
								continue;
							}
							goto IL_322;
						case 21:
							if (!enumerator.MoveNext())
							{
								num = 3;
								continue;
							}
							iniSetting = (IniSetting)enumerator.Current;
							num = 7;
							continue;
						case 22:
							A_1.WriteElementString(HyperlinksCollectionEditor.b("紨䨪䨬", a_), iniSetting.Tag);
							num = 0;
							continue;
						case 23:
							A_1.WriteElementString(HyperlinksCollectionEditor.b("洨个帬䰮", a_), iniSetting.Description);
							num = 25;
							continue;
						case 24:
							if (spr\u1DE5.ᜎ == null)
							{
								num = 10;
								continue;
							}
							goto IL_3E4;
						case 25:
							goto IL_5CE;
						case 26:
						{
							string typeName;
							int num2;
							if (spr\u1DE5.ᜎ.TryGetValue(typeName, out num2))
							{
								num = 30;
								continue;
							}
							goto IL_4C0;
						}
						case 27:
							A_1.WriteStartElement(HyperlinksCollectionEditor.b("稨个夬嬮堰崲刴", a_));
							A_1.WriteAttributeString(HyperlinksCollectionEditor.b("木䨪䀬䨮", a_), iniSetting.Name);
							A_1.WriteAttributeString(HyperlinksCollectionEditor.b("紨刪崬䨮", a_), iniSetting.TypeName);
							num = 1;
							continue;
						case 28:
							goto IL_492;
						case 30:
							num = 19;
							continue;
						case 32:
							goto IL_492;
						case 33:
							goto IL_492;
						case 34:
							goto IL_3E4;
						}
						goto IL_EF;
						IL_322:
						num = 15;
						continue;
						IL_350:
						num = 21;
						continue;
						IL_EF:
						goto IL_350;
						IL_3E4:
						num = 26;
						continue;
						IL_492:
						num = 20;
						continue;
						IL_4C0:
						A_1.WriteElementString(HyperlinksCollectionEditor.b("缨䨪䄬娮吰", a_), iniSetting);
						num = 5;
						continue;
						IL_5CE:
						A_1.WriteEndElement();
						num = 29;
					}
					IL_6DC:;
				}
				finally
				{
					for (;;)
					{
						IDisposable disposable = enumerator as IDisposable;
						int num = 0;
						for (;;)
						{
							switch (num)
							{
							case 0:
								if (disposable != null)
								{
									num = 1;
									continue;
								}
								goto IL_725;
							case 1:
								disposable.Dispose();
								num = 2;
								continue;
							case 2:
								goto IL_723;
							}
							break;
						}
					}
					IL_723:
					IL_725:;
				}
				break;
			}
			}
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x000BBC2C File Offset: 0x000BAC2C
		private void ᜀ(ArrayList A_0, ref XmlTextWriter A_1)
		{
			int a_ = 11;
			for (;;)
			{
				int count = A_0.Count;
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						int num2;
						if (num2 > count - 1)
						{
							num = 3;
							continue;
						}
						A_1.WriteElementString(HyperlinksCollectionEditor.b("渦崨个䀬", a_), A_0[num2].ToString());
						num2++;
						num = 2;
						continue;
					}
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3D;
						default:
							if (false)
							{
							}
							goto IL_F2;
						}
						break;
					case 2:
						goto IL_F2;
					case 3:
						goto IL_111;
					case 4:
					{
						if (count == 0)
						{
							goto IL_3D;
						}
						A_1.WriteStartElement(HyperlinksCollectionEditor.b("昦嬨太䰬嘮細娲䘴䌶", a_));
						A_1.WriteAttributeString(HyperlinksCollectionEditor.b("猦倨嬪䠬", a_), A_0[0].GetType().Name);
						int num2 = 0;
						num = 1;
						continue;
					}
					case 5:
						goto IL_45;
					}
					break;
					IL_3D:
					num = 5;
					continue;
					IL_F2:
					num = 0;
				}
			}
			IL_45:
			if (true)
			{
			}
			return;
			IL_111:
			A_1.WriteEndElement();
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x000BBD54 File Offset: 0x000BAD54
		private bool ᜄ(string A_0)
		{
			int a_ = 5;
			switch (0)
			{
			default:
			{
				bool result = true;
				XmlTextReader xmlTextReader = new XmlTextReader(A_0);
				this.LastXmlError = "";
				this.ᜃ = "";
				try
				{
					for (;;)
					{
						xmlTextReader.WhitespaceHandling = WhitespaceHandling.None;
						xmlTextReader.NameTable.Add(HyperlinksCollectionEditor.b("爠䘢䘤匦䀨䐪䌬", a_));
						xmlTextReader.NameTable.Add(HyperlinksCollectionEditor.b("爠䘢䘤匦䀨䐪䌬尮", a_));
						xmlTextReader.NameTable.Add(HyperlinksCollectionEditor.b("爠䘢儤匦䀨䔪䨬", a_));
						xmlTextReader.NameTable.Add(HyperlinksCollectionEditor.b("爠䘢儤匦䀨䔪䨬尮", a_));
						xmlTextReader.NameTable.Add(HyperlinksCollectionEditor.b("漠䈢䠤䈦", a_));
						xmlTextReader.NameTable.Add(HyperlinksCollectionEditor.b("甠娢唤䈦", a_));
						xmlTextReader.NameTable.Add(HyperlinksCollectionEditor.b("眠䈢䤤刦䰨", a_));
						xmlTextReader.NameTable.Add(HyperlinksCollectionEditor.b("怠儢圤䘦倨未䐬尮䔰", a_));
						xmlTextReader.NameTable.Add(HyperlinksCollectionEditor.b("栠圢䀤䨦", a_));
						xmlTextReader.NameTable.Add(HyperlinksCollectionEditor.b("爠䘢儤匦䀨䔪䨬尮", a_));
						int num = 4;
						for (;;)
						{
							switch (num)
							{
							case 0:
								if (!xmlTextReader.Read())
								{
									num = 9;
									continue;
								}
								num = 8;
								continue;
							case 1:
								this.ᜂ(null, ref xmlTextReader);
								num = 11;
								continue;
							case 2:
								if (xmlTextReader.NodeType == XmlNodeType.Element)
								{
									num = 6;
									continue;
								}
								goto IL_1C0;
							case 3:
								goto IL_289;
							case 4:
								goto IL_1C0;
							case 5:
								goto IL_257;
							case 6:
								num = 7;
								continue;
							case 7:
								if (xmlTextReader.Name == HyperlinksCollectionEditor.b("爠䘢䘤匦䀨䐪䌬尮", a_))
								{
									num = 1;
									continue;
								}
								goto IL_1C0;
							case 8:
								if (xmlTextReader.NodeType == XmlNodeType.Comment)
								{
									num = 10;
									continue;
								}
								goto IL_257;
							case 9:
								num = 3;
								continue;
							case 10:
								this.ᜃ = this.ᜃ + xmlTextReader.Value + HyperlinksCollectionEditor.b("⬠", a_);
								num = 5;
								continue;
							case 11:
								goto IL_1C0;
							}
							break;
							IL_1C0:
							num = 0;
							continue;
							IL_257:
							num = 2;
						}
					}
					IL_289:;
				}
				catch (Exception ex)
				{
					this.LastXmlError = string.Concat(new object[]
					{
						HyperlinksCollectionEditor.b("戠䈢䬤䤦䘨弪ബ崮吰刲儴᜶䨸帺䤼䬾⡀ⵂ≄㑆䍈敊浌", a_),
						ex.Message,
						HyperlinksCollectionEditor.b("⬠漢䰤䤦䰨ପ", a_),
						xmlTextReader.LineNumber
					});
					result = false;
				}
				finally
				{
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 1:
							xmlTextReader.Close();
							num = 2;
							continue;
						case 2:
							goto IL_335;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_337;
						default:
							if (false)
							{
							}
							if (xmlTextReader == null)
							{
								goto IL_337;
							}
							num = 1;
							break;
						}
					}
					IL_335:
					IL_337:;
				}
				if (true)
				{
				}
				return result;
			}
			}
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x000BC0E0 File Offset: 0x000BB0E0
		private void ᜂ(IniSetting A_0, ref XmlTextReader A_1)
		{
			int a_ = 2;
			for (;;)
			{
				int depth = A_1.Depth;
				int num = 9;
				for (;;)
				{
					IniSection iniSection;
					switch (num)
					{
					case 0:
						goto IL_E6;
					case 1:
						goto IL_191;
					case 2:
						if (!A_1.Read())
						{
							num = 6;
							continue;
						}
						num = 12;
						continue;
					case 3:
						return;
					case 4:
						return;
					case 5:
						goto IL_8B;
					case 6:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_191;
						default:
							goto IL_1DE;
						}
						break;
					case 7:
						num = 1;
						continue;
					case 8:
					{
						string attribute = A_1.GetAttribute(HyperlinksCollectionEditor.b("倝䄟伡䄣", a_));
						num = 13;
						continue;
					}
					case 9:
						goto IL_E6;
					case 10:
					{
						if (A_0 == null)
						{
							num = 14;
							continue;
						}
						string attribute;
						iniSection = this.Sections.Add(attribute);
						A_0.Value = iniSection;
						num = 5;
						continue;
					}
					case 11:
						goto IL_8B;
					case 12:
						if (depth == A_1.Depth)
						{
							num = 3;
							continue;
						}
						num = 15;
						continue;
					case 13:
					{
						string attribute;
						if (attribute.Length == 0)
						{
							num = 4;
							continue;
						}
						num = 10;
						continue;
					}
					case 14:
					{
						string attribute;
						iniSection = this.Sections.Add(attribute);
						num = 11;
						continue;
					}
					case 15:
						if (A_1.NodeType == XmlNodeType.Element)
						{
							num = 7;
							continue;
						}
						goto IL_E6;
					}
					break;
					IL_8B:
					this.ᜀ(iniSection, ref A_1);
					num = 0;
					continue;
					IL_E6:
					num = 2;
					continue;
					IL_191:
					if (!(A_1.Name == HyperlinksCollectionEditor.b("䴝䔟䄡倣伥䜧䐩", a_)))
					{
						goto IL_E6;
					}
					if (true)
					{
					}
					num = 8;
				}
			}
			return;
			IL_1DE:
			if (false)
			{
			}
		}

		// Token: 0x0600116A RID: 4458 RVA: 0x000BC2D4 File Offset: 0x000BB2D4
		private void ᜁ(IniSetting A_0, ref XmlTextReader A_1)
		{
			int a_ = 11;
			for (;;)
			{
				IL_39:
				IniSettings iniSettings = new IniSettings(this);
				A_0.Value = iniSettings;
				int depth = A_1.Depth;
				for (;;)
				{
					IL_5B:
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							num = 9;
							continue;
						case 1:
							goto IL_DC;
						case 2:
							if (depth == A_1.Depth)
							{
								num = 4;
								continue;
							}
							num = 5;
							continue;
						case 3:
							return;
						case 4:
							return;
						case 5:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_5B;
							default:
								if (false)
								{
								}
								if (A_1.NodeType == XmlNodeType.Element)
								{
									num = 0;
									continue;
								}
								goto IL_DC;
							}
							break;
						case 6:
							this.ᜀ(iniSettings, ref A_1);
							num = 7;
							continue;
						case 7:
							if (true)
							{
							}
							goto IL_DC;
						case 8:
							if (!A_1.Read())
							{
								num = 3;
								continue;
							}
							num = 2;
							continue;
						case 9:
							if (A_1.Name == HyperlinksCollectionEditor.b("琦䰨弪夬䘮弰吲", a_))
							{
								num = 6;
								continue;
							}
							goto IL_DC;
						}
						goto IL_39;
						IL_DC:
						num = 8;
					}
				}
			}
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x000BC418 File Offset: 0x000BB418
		private void ᜀ(IniSetting A_0, ref XmlTextReader A_1)
		{
			int a_ = 2;
			ArrayList arrayList;
			for (;;)
			{
				int num = 0;
				arrayList = new ArrayList();
				int num2 = 8;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						string attribute;
						arrayList.Add(this.ᜀ(A_1.ReadElementString(), attribute));
						num2 = 14;
						continue;
					}
					case 1:
						return;
					case 2:
						num2 = 6;
						continue;
					case 3:
						goto IL_7F;
					case 4:
						if (A_1.NodeType == XmlNodeType.Element)
						{
							num2 = 2;
							continue;
						}
						goto IL_F6;
					case 5:
						if (num == A_1.Depth)
						{
							num2 = 3;
							continue;
						}
						goto IL_16C;
					case 6:
						if (A_1.Name == HyperlinksCollectionEditor.b("弝刟倡䔣弥搧䌩弫娭", a_))
						{
							num2 = 9;
							continue;
						}
						goto IL_F6;
					case 7:
						if (A_1.NodeType != XmlNodeType.Element)
						{
							goto IL_62;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_150;
						default:
							if (false)
							{
							}
							num2 = 13;
							continue;
						}
						break;
					case 8:
						num2 = 12;
						continue;
					case 9:
					{
						num = A_1.Depth;
						string attribute = A_1.GetAttribute(HyperlinksCollectionEditor.b("䨝够刡䄣", a_));
						A_1.Read();
						num2 = 10;
						continue;
					}
					case 10:
						goto IL_16C;
					case 11:
						if (A_1.Name == HyperlinksCollectionEditor.b("圝吟䜡䤣", a_))
						{
							num2 = 0;
							continue;
						}
						goto IL_62;
					case 12:
						goto IL_150;
					case 13:
						num2 = 11;
						continue;
					case 14:
						goto IL_62;
					}
					break;
					IL_62:
					num2 = 5;
					continue;
					IL_150:
					if (!A_1.Read())
					{
						num2 = 1;
						continue;
					}
					num2 = 4;
					continue;
					IL_16C:
					if (true)
					{
					}
					num2 = 7;
				}
			}
			IL_7F:
			IL_F6:
			A_0.Value = arrayList;
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x000BC610 File Offset: 0x000BB610
		private void ᜁ(IniSettings A_0, ref XmlTextReader A_1)
		{
			int a_ = 12;
			for (;;)
			{
				IL_39:
				int depth = A_1.Depth;
				for (;;)
				{
					IL_4D:
					int num = 7;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (A_1.Name == HyperlinksCollectionEditor.b("笧伩堫娭夯就匳", a_))
							{
								num = 6;
								continue;
							}
							goto IL_C6;
						case 1:
							return;
						case 2:
							goto IL_C4;
						case 3:
							goto IL_C6;
						case 4:
							num = 0;
							continue;
						case 5:
							if (!A_1.Read())
							{
								num = 1;
								continue;
							}
							num = 9;
							continue;
						case 6:
							this.ᜀ(A_0, ref A_1);
							num = 3;
							continue;
						case 7:
							goto IL_C6;
						case 8:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_4D;
							default:
								if (false)
								{
								}
								if (A_1.NodeType == XmlNodeType.Element)
								{
									num = 4;
									continue;
								}
								goto IL_C6;
							}
							break;
						case 9:
							if (depth == A_1.Depth)
							{
								num = 2;
								continue;
							}
							num = 8;
							continue;
						}
						goto IL_39;
						IL_C6:
						num = 5;
					}
				}
			}
			IL_C4:
			if (true)
			{
			}
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x000BC744 File Offset: 0x000BB744
		private void ᜀ(IniSection A_0, ref XmlTextReader A_1)
		{
			int a_ = 6;
			for (;;)
			{
				IL_39:
				int depth = A_1.Depth;
				for (;;)
				{
					IL_4D:
					int num = 8;
					for (;;)
					{
						switch (num)
						{
						case 0:
							num = 9;
							continue;
						case 1:
							return;
						case 2:
							if (depth == A_1.Depth)
							{
								num = 3;
								continue;
							}
							num = 7;
							continue;
						case 3:
							return;
						case 4:
							if (!A_1.Read())
							{
								num = 1;
								continue;
							}
							num = 2;
							continue;
						case 5:
							goto IL_D3;
						case 6:
							this.ᜀ(A_0.Settings, ref A_1);
							num = 5;
							continue;
						case 7:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_4D;
							default:
								if (false)
								{
								}
								if (A_1.NodeType == XmlNodeType.Element)
								{
									num = 0;
									continue;
								}
								goto IL_D3;
							}
							break;
						case 8:
							if (true)
							{
							}
							goto IL_D3;
						case 9:
							if (A_1.Name == HyperlinksCollectionEditor.b("無䄣別尧䌩䈫䤭", a_))
							{
								num = 6;
								continue;
							}
							goto IL_D3;
						}
						goto IL_39;
						IL_D3:
						num = 4;
					}
				}
			}
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x000BC880 File Offset: 0x000BB880
		private void ᜀ(IniSetting A_0, string A_1, ref XmlTextReader A_2)
		{
			int a_ = 4;
			for (;;)
			{
				if (true)
				{
				}
				int depth = A_2.Depth;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						return;
					case 2:
						if (!A_2.Read())
						{
							num = 0;
							continue;
						}
						num = 5;
						continue;
					case 3:
						goto IL_16C;
					case 4:
						if (A_2.Name == HyperlinksCollectionEditor.b("瘟䌡䠣匥䴧", a_))
						{
							num = 11;
							continue;
						}
						goto IL_193;
					case 5:
						if (A_2.NodeType == XmlNodeType.Element)
						{
							num = 12;
							continue;
						}
						goto IL_16C;
					case 6:
						if (depth == A_2.Depth)
						{
							num = 8;
							continue;
						}
						goto IL_74;
					case 7:
						if (depth == A_2.Depth)
						{
							num = 1;
							continue;
						}
						goto IL_193;
					case 8:
						return;
					case 9:
						return;
					case 10:
						if (A_2.Name == HyperlinksCollectionEditor.b("搟䜡圣䔥", a_))
						{
							num = 13;
							continue;
						}
						goto IL_16C;
					case 11:
						A_0.Value = this.ᜀ(A_2.ReadElementString(), A_1);
						num = 7;
						continue;
					case 12:
						num = 4;
						continue;
					case 13:
						A_0.Description = A_2.ReadElementString();
						num = 16;
						continue;
					case 14:
						if (A_2.Name == HyperlinksCollectionEditor.b("琟䌡䌣", a_))
						{
							num = 15;
							continue;
						}
						goto IL_74;
					case 15:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (false)
							{
							}
							A_0.Tag = A_2.ReadElementString();
							num = 6;
							continue;
						}
						break;
					case 16:
						if (depth == A_2.Depth)
						{
							num = 9;
							continue;
						}
						goto IL_16C;
					}
					break;
					IL_74:
					num = 10;
					continue;
					IL_16C:
					num = 2;
					continue;
					IL_193:
					num = 14;
				}
			}
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x000BCABC File Offset: 0x000BBABC
		private void ᜀ(IniSettings A_0, ref XmlTextReader A_1)
		{
			int a_ = 9;
			switch (0)
			{
			default:
			{
				string attribute2;
				IniSetting a_2;
				for (;;)
				{
					string attribute = A_1.GetAttribute(HyperlinksCollectionEditor.b("欤䘦䐨个", a_));
					attribute2 = A_1.GetAttribute(HyperlinksCollectionEditor.b("焤带夨个", a_));
					a_2 = A_0.Add(attribute);
					int num = 8;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_DA;
						case 1:
						{
							string a;
							if (!(a == HyperlinksCollectionEditor.b("搤唦嬨䨪听挮堰䀲䄴", a_)))
							{
								num = 4;
								continue;
							}
							goto IL_10F;
						}
						case 2:
							num = 6;
							continue;
						case 3:
							num = 1;
							continue;
						case 4:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								num = 0;
								continue;
							}
							break;
						case 5:
							num = 7;
							continue;
						case 6:
						{
							string a;
							if (!(a == HyperlinksCollectionEditor.b("氤䤦䀨砪䠬嬮䔰娲嬴倶䨸", a_)))
							{
								num = 5;
								continue;
							}
							goto IL_A9;
						}
						case 7:
						{
							string a;
							if (!(a == HyperlinksCollectionEditor.b("氤䤦䀨砪䠬䰮䔰娲娴夶", a_)))
							{
								num = 3;
								continue;
							}
							goto IL_153;
						}
						case 8:
						{
							string a;
							if ((a = attribute2) != null)
							{
								num = 2;
								continue;
							}
							goto IL_18F;
						}
						}
						break;
					}
				}
				IL_A9:
				this.ᜁ(a_2, ref A_1);
				return;
				IL_DA:
				goto IL_18F;
				IL_10F:
				if (true)
				{
				}
				this.ᜀ(a_2, ref A_1);
				return;
				IL_153:
				this.ᜂ(a_2, ref A_1);
				return;
				IL_18F:
				this.ᜀ(a_2, attribute2, ref A_1);
				return;
			}
			}
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x000BCC64 File Offset: 0x000BBC64
		private object ᜀ(string A_0, string A_1)
		{
			int a_ = 2;
			int num = 8;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_76;
				case 1:
				{
					int num2;
					if (spr\u1DE5.ᜏ.TryGetValue(A_1, out num2))
					{
						num = 6;
						continue;
					}
					return A_0;
				}
				case 2:
				{
					int num2;
					switch (num2)
					{
					case 0:
						goto IL_10A;
					case 1:
						goto IL_1D1;
					case 2:
						goto IL_1DD;
					case 3:
						goto IL_134;
					case 4:
						goto IL_201;
					case 5:
						goto IL_F9;
					case 6:
						goto IL_212;
					case 7:
						goto IL_62;
					case 8:
						goto IL_223;
					case 9:
						goto IL_E0;
					case 10:
						goto IL_56;
					case 11:
						goto IL_1F5;
					case 12:
						goto IL_1E9;
					case 13:
						goto IL_9F;
					case 14:
						goto IL_116;
					case 15:
						goto IL_EC;
					case 16:
						goto IL_123;
					default:
						num = 5;
						continue;
					}
					break;
				}
				case 3:
					if (spr\u1DE5.ᜏ == null)
					{
						num = 4;
						continue;
					}
					goto IL_A7;
				case 4:
					spr\u1DE5.ᜏ = new Dictionary<string, int>(17)
					{
						{
							HyperlinksCollectionEditor.b("尝伟䴡䠣䌥䤧䐩", a_),
							0
						},
						{
							HyperlinksCollectionEditor.b("尝够嘡䄣", a_),
							1
						},
						{
							HyperlinksCollectionEditor.b("崝䠟䌡嘣", a_),
							2
						},
						{
							HyperlinksCollectionEditor.b("娝䄟嘡䄣爥䄧䜩䤫", a_),
							3
						},
						{
							HyperlinksCollectionEditor.b("娝䔟䄡䴣䬥䤧䘩", a_),
							4
						},
						{
							HyperlinksCollectionEditor.b("娝伟圡䘣䨥䴧", a_),
							5
						},
						{
							HyperlinksCollectionEditor.b("䴝䤟䰡䌣䨥䴧", a_),
							6
						},
						{
							HyperlinksCollectionEditor.b("圝丟嘡ᔣဥ", a_),
							7
						},
						{
							HyperlinksCollectionEditor.b("圝丟嘡ᜣᐥ", a_),
							8
						},
						{
							HyperlinksCollectionEditor.b("圝丟嘡ሣሥ", a_),
							9
						},
						{
							HyperlinksCollectionEditor.b("䬝椟䰡倣ᜥḧ", a_),
							10
						},
						{
							HyperlinksCollectionEditor.b("䬝椟䰡倣ᔥᨧ", a_),
							11
						},
						{
							HyperlinksCollectionEditor.b("䬝椟䰡倣ဥᰧ", a_),
							12
						},
						{
							HyperlinksCollectionEditor.b("堝伟䰡倣", a_),
							13
						},
						{
							HyperlinksCollectionEditor.b("䴝䤟堡䄣", a_),
							14
						},
						{
							HyperlinksCollectionEditor.b("丝伟䬡䨣別", a_),
							15
						},
						{
							HyperlinksCollectionEditor.b("崝伟両䬣吥", a_),
							16
						}
					};
					num = 7;
					continue;
				case 5:
					num = 0;
					continue;
				case 6:
					num = 2;
					continue;
				case 7:
					goto IL_A7;
				case 9:
					num = 3;
					continue;
				}
				if (A_1 != null)
				{
					num = 9;
					continue;
				}
				return A_0;
				IL_A7:
				num = 1;
			}
			IL_56:
			return Convert.ToUInt16(A_0);
			IL_62:
			return Convert.ToInt16(A_0);
			IL_76:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_E0:
				return Convert.ToInt64(A_0);
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				return A_0;
			}
			IL_9F:
			return this.ᜃ(A_0);
			IL_EC:
			return this.ᜂ(A_0);
			IL_F9:
			return Convert.ToDouble(A_0, NumberFormatInfo.InvariantInfo);
			IL_10A:
			return Convert.ToBoolean(A_0);
			IL_116:
			return this.ᜁ(A_0);
			IL_123:
			return Color.FromArgb(Convert.ToInt32(A_0));
			IL_134:
			return Convert.ToDateTime(A_0, DateTimeFormatInfo.InvariantInfo);
			IL_1D1:
			return Convert.ToByte(A_0);
			IL_1DD:
			return Convert.ToChar(A_0);
			IL_1E9:
			return Convert.ToUInt64(A_0);
			IL_1F5:
			return Convert.ToUInt32(A_0);
			IL_201:
			return Convert.ToDecimal(A_0, NumberFormatInfo.InvariantInfo);
			IL_212:
			return Convert.ToSingle(A_0, NumberFormatInfo.InvariantInfo);
			IL_223:
			return Convert.ToInt32(A_0);
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x000BD02C File Offset: 0x000BC02C
		private string ᜀ(Font A_0)
		{
			int a_ = 7;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			StringBuilder stringBuilder = new StringBuilder(128);
			stringBuilder.Append(A_0.FontFamily.Name);
			stringBuilder.Append(HyperlinksCollectionEditor.b("弢", a_));
			stringBuilder.Append(A_0.Size);
			stringBuilder.Append(HyperlinksCollectionEditor.b("弢", a_));
			stringBuilder.Append(A_0.Style);
			stringBuilder.Append(HyperlinksCollectionEditor.b("弢", a_));
			stringBuilder.Append(A_0.Unit);
			stringBuilder.Append(HyperlinksCollectionEditor.b("弢", a_));
			stringBuilder.Append(A_0.GdiCharSet);
			stringBuilder.Append(HyperlinksCollectionEditor.b("弢", a_));
			stringBuilder.Append(A_0.GdiVerticalFont);
			return stringBuilder.ToString();
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x000BD148 File Offset: 0x000BC148
		private Font ᜃ(string A_0)
		{
			Font result;
			try
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				char c = '|';
				string[] array = A_0.Split(new char[]
				{
					c
				});
				result = new Font(array[0], float.Parse(array[1]), (FontStyle)Enum.Parse(typeof(FontStyle), array[2], true), (GraphicsUnit)Enum.Parse(typeof(GraphicsUnit), array[3], true), Convert.ToByte(array[4]), Convert.ToBoolean(array[5]));
			}
			catch
			{
				result = null;
			}
			if (true)
			{
			}
			return result;
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x000BD204 File Offset: 0x000BC204
		private string ᜀ(Point A_0)
		{
			int a_ = 18;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return A_0.X + HyperlinksCollectionEditor.b("ȭ", a_) + A_0.Y;
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x000BD274 File Offset: 0x000BC274
		private Point ᜂ(string A_0)
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
			char c = ',';
			return new Point(int.Parse(A_0.Split(new char[]
			{
				c
			})[0]), int.Parse(A_0.Split(new char[]
			{
				c
			})[1]));
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x000BD2EC File Offset: 0x000BC2EC
		private string ᜀ(Size A_0)
		{
			int a_ = 2;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return A_0.Width + HyperlinksCollectionEditor.b("㈝", a_) + A_0.Height;
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x000BD35C File Offset: 0x000BC35C
		private Size ᜁ(string A_0)
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
			char c = ',';
			return new Size(int.Parse(A_0.Split(new char[]
			{
				c
			})[0]), int.Parse(A_0.Split(new char[]
			{
				c
			})[1]));
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x000BD3D4 File Offset: 0x000BC3D4
		private string ᜀ()
		{
			int a_ = 17;
			string text;
			for (;;)
			{
				if (true)
				{
				}
				text = "";
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_8F;
				default:
				{
					if (false)
					{
					}
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (text.IndexOf(HyperlinksCollectionEditor.b("䐬䌮吰ल椴", a_)) >= 0)
							{
								num = 2;
								continue;
							}
							goto IL_B4;
						case 1:
							try
							{
								text = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
								goto IL_51;
							}
							catch
							{
								goto IL_51;
							}
							goto IL_8F;
							IL_51:
							num = 0;
							continue;
						case 2:
							goto IL_78;
						}
						break;
					}
					break;
				}
				}
			}
			IL_78:
			IL_8F:
			return text.Substring(6) + HyperlinksCollectionEditor.b("焬", a_);
			IL_B4:
			return text + HyperlinksCollectionEditor.b("焬", a_);
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x000BD4BC File Offset: 0x000BC4BC
		private string ᜀ(string A_0)
		{
			int a_ = 6;
			string result;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return result;
			}
			if (false)
			{
			}
			if (true)
			{
			}
			string text = "";
			try
			{
				int num = 14;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_21F;
					case 1:
						goto IL_DB;
					case 2:
						if (Directory.Exists(A_0.Substring(0, A_0.LastIndexOf(HyperlinksCollectionEditor.b("縡", a_)))))
						{
							num = 5;
							continue;
						}
						goto IL_17F;
					case 3:
						text += HyperlinksCollectionEditor.b("డ尣䬥䐧", a_);
						num = 0;
						continue;
					case 4:
						goto IL_DB;
					case 5:
						num = 11;
						continue;
					case 6:
						num = 2;
						continue;
					case 7:
						if (this.ᜆ != null)
						{
							num = 9;
							continue;
						}
						text = this.ᜀ() + A_0;
						num = 1;
						continue;
					case 8:
						goto IL_21F;
					case 9:
						text = this.ᜆ + A_0;
						num = 4;
						continue;
					case 10:
						goto IL_210;
					case 11:
						if (!text.EndsWith(HyperlinksCollectionEditor.b("డ尣䬥䐧", a_)))
						{
							num = 15;
							continue;
						}
						goto IL_210;
					case 12:
						goto IL_22C;
					case 13:
						if (!text.EndsWith(HyperlinksCollectionEditor.b("డ尣䬥䐧", a_)))
						{
							num = 3;
							continue;
						}
						goto IL_21F;
					case 15:
						text += HyperlinksCollectionEditor.b("డ尣䬥䐧", a_);
						num = 10;
						continue;
					}
					if (A_0.IndexOf(HyperlinksCollectionEditor.b("縡", a_)) >= 0)
					{
						num = 6;
						continue;
					}
					num = 7;
					continue;
					IL_DB:
					num = 13;
					continue;
					IL_210:
					text = A_0;
					num = 8;
					continue;
					IL_21F:
					result = text;
					num = 12;
				}
				IL_17F:
				throw new ApplicationException(HyperlinksCollectionEditor.b("搡䬣䨥䰧伩師อ启崱儳䔵ᠷ吹医䨽怿❁㱃⽅㭇㹉手", a_));
				IL_22C:;
			}
			catch
			{
				throw;
			}
			return result;
		}

		// Token: 0x04000C3A RID: 3130
		private IniSections ᜀ;

		// Token: 0x04000C3B RID: 3131
		private string ᜁ;

		// Token: 0x04000C3C RID: 3132
		private bool \u25D9\u00A4\u0084\u00A5;

		// Token: 0x04000C3D RID: 3133
		private string ᜂ;

		// Token: 0x04000C3E RID: 3134
		private byte[] \u25D9\u009D\u0082\u008D;

		// Token: 0x04000C3F RID: 3135
		private int[] \u2460\u0090\u009C\u00A9;

		// Token: 0x04000C40 RID: 3136
		private string ᜃ;

		// Token: 0x04000C41 RID: 3137
		internal bool ᜄ;

		// Token: 0x04000C42 RID: 3138
		private long \u2609\u0098\u0096\u0080;

		// Token: 0x04000C43 RID: 3139
		private DateTime ᜅ;

		// Token: 0x04000C44 RID: 3140
		private string ᜆ;

		// Token: 0x04000C45 RID: 3141
		private string[] \u2460\u0095\u0084\u0083;

		// Token: 0x04000C46 RID: 3142
		private float[] \u2609\u008B\u0080\u0088;

		// Token: 0x04000C47 RID: 3143
		public string LastXmlError;
	}
}
