using System;
using System.Collections;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Spire.DataExport.CollectionEditors;

namespace Spire.DataExport.Utils
{
	// Token: 0x02000236 RID: 566
	public class AppSettingsUtils
	{
		// Token: 0x06001130 RID: 4400 RVA: 0x000B86AC File Offset: 0x000B76AC
		public static bool AddPropertyToBag(XMLSetting XmlXmlSetting, Control control, string propertyName)
		{
			int a_ = 18;
			bool result;
			try
			{
				switch (0)
				{
				default:
					for (;;)
					{
						IniSection iniSection = XmlXmlSetting.Sections.Add(HyperlinksCollectionEditor.b("縭䈯崱䐳匵䨷丹䔻簽ℿ╁", a_));
						string text = AppSettingsUtils.ᜀ(control);
						int num = 5;
						for (;;)
						{
							IniSetting iniSetting;
							IniSettings iniSettings;
							switch (num)
							{
							case 0:
								iniSetting.Value = control.GetType().GetProperty(propertyName).GetValue(control, null);
								num = 4;
								continue;
							case 1:
								goto IL_83;
							case 2:
								if (iniSetting.Value != null)
								{
									goto IL_144;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_D4;
								default:
									if (false)
									{
									}
									num = 0;
									continue;
								}
								break;
							case 3:
								iniSettings = iniSection.Settings.Add(text, new IniSettings(XmlXmlSetting));
								num = 6;
								continue;
							case 4:
								goto IL_144;
							case 5:
								if (iniSection.Settings[text] == null)
								{
									num = 3;
									continue;
								}
								goto IL_D4;
							case 6:
								goto IL_83;
							case 7:
								goto IL_153;
							}
							break;
							IL_83:
							if (true)
							{
							}
							iniSetting = iniSettings.Add(propertyName);
							num = 2;
							continue;
							IL_D4:
							iniSettings = iniSection.Settings[text];
							num = 1;
							continue;
							IL_144:
							result = true;
							num = 7;
						}
					}
					IL_153:
					break;
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x000B883C File Offset: 0x000B783C
		private static Control ᜀ(Control A_0, string A_1)
		{
			switch (0)
			{
			default:
			{
				Control control;
				for (;;)
				{
					IL_35:
					string[] array = A_1.Split(new char[]
					{
						'.'
					});
					int num = array.Length;
					control = A_0;
					int num2 = 1;
					int num3;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_97:
						num3 = 0;
						break;
					default:
						if (false)
						{
						}
						num3 = 1;
						break;
					}
					IEnumerator enumerator;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							try
							{
								num3 = 0;
								for (;;)
								{
									switch (num3)
									{
									case 1:
										goto IL_15F;
									case 2:
									{
										Control control2;
										if (control2.Name == array[num2])
										{
											num3 = 4;
											continue;
										}
										break;
									}
									case 3:
										goto IL_16B;
									case 4:
									{
										Control control2;
										control = control2;
										num3 = 5;
										continue;
									}
									case 5:
										goto IL_15F;
									case 6:
									{
										if (!enumerator.MoveNext())
										{
											num3 = 1;
											continue;
										}
										Control control2 = (Control)enumerator.Current;
										num3 = 2;
										continue;
									}
									}
									IL_FE:
									num3 = 6;
									continue;
									goto IL_FE;
									IL_15F:
									num3 = 3;
								}
								IL_16B:
								goto IL_7B;
							}
							finally
							{
								for (;;)
								{
									IDisposable disposable = enumerator as IDisposable;
									num3 = 0;
									for (;;)
									{
										switch (num3)
										{
										case 0:
											if (disposable != null)
											{
												num3 = 2;
												continue;
											}
											goto IL_1B8;
										case 1:
											goto IL_1B6;
										case 2:
											disposable.Dispose();
											num3 = 1;
											continue;
										}
										break;
									}
								}
								IL_1B6:
								IL_1B8:;
							}
							return control;
							IL_7B:
							num2++;
							num3 = 2;
							continue;
						case 1:
							goto IL_A5;
						case 2:
							goto IL_A5;
						case 3:
							if (num2 >= num)
							{
								if (true)
								{
								}
								num3 = 4;
								continue;
							}
							goto IL_8A;
						case 4:
							return control;
						}
						goto IL_35;
						IL_A5:
						num3 = 3;
					}
					IL_8A:
					enumerator = control.Controls.GetEnumerator();
					goto IL_97;
				}
				return control;
			}
			}
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x000B8A14 File Offset: 0x000B7A14
		private static string ᜀ(Control A_0)
		{
			int a_ = 17;
			StringBuilder stringBuilder;
			for (;;)
			{
				for (;;)
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
						stringBuilder = new StringBuilder(128);
						stringBuilder.Insert(0, A_0.Name);
						Control control = A_0;
						int num = 3;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_84;
							case 1:
								goto IL_6C;
							case 2:
								if (control.Parent == null)
								{
									num = 0;
									continue;
								}
								control = control.Parent;
								stringBuilder.Insert(0, HyperlinksCollectionEditor.b("̬", a_));
								stringBuilder.Insert(0, control.Name);
								if (true)
								{
								}
								num = 1;
								continue;
							case 3:
								goto IL_6C;
							}
							break;
							IL_6C:
							num = 2;
						}
						break;
					}
					}
				}
			}
			IL_84:
			return stringBuilder.ToString();
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x000B8AF0 File Offset: 0x000B7AF0
		public static bool SetAllPropertiesFromBag(XMLSetting XmlXmlSetting, Control context)
		{
			int a_ = 6;
			bool result;
			try
			{
				switch (0)
				{
				default:
				{
					IniSection iniSection = XmlXmlSetting.Sections[HyperlinksCollectionEditor.b("爡嘣䤥堧伩師娭䤯瀱唳儵", a_)];
					AppSettingsUtils.ᜀ(context);
					IEnumerator enumerator = iniSection.Settings.GetEnumerator();
					try
					{
						int num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								if (!enumerator.MoveNext())
								{
									num = 5;
									continue;
								}
								IniSetting iniSetting = (IniSetting)enumerator.Current;
								num = 4;
								continue;
							}
							case 1:
								try
								{
									num = 4;
									for (;;)
									{
										switch (num)
										{
										case 1:
										{
											PropertyInfo property;
											if (property.CanWrite)
											{
												num = 8;
												continue;
											}
											break;
										}
										case 2:
										{
											PropertyInfo property;
											Control control;
											IniSetting iniSetting2;
											property.SetValue(control, iniSetting2.Value, null);
											num = 0;
											continue;
										}
										case 3:
										{
											IniSetting iniSetting2;
											if (iniSetting2.Value != null)
											{
												num = 2;
												continue;
											}
											break;
										}
										case 5:
										{
											IEnumerator enumerator2;
											if (!enumerator2.MoveNext())
											{
												num = 7;
												continue;
											}
											IniSetting iniSetting2 = (IniSetting)enumerator2.Current;
											Type type;
											PropertyInfo property = type.GetProperty(iniSetting2.Name);
											num = 1;
											continue;
										}
										case 6:
											goto IL_1F7;
										case 7:
											num = 6;
											continue;
										case 8:
											num = 3;
											continue;
										}
										IL_1A2:
										num = 5;
										continue;
										goto IL_1A2;
									}
									IL_1F7:
									break;
								}
								finally
								{
									for (;;)
									{
										IEnumerator enumerator2;
										IDisposable disposable = enumerator2 as IDisposable;
										num = 1;
										for (;;)
										{
											switch (num)
											{
											case 0:
												goto IL_242;
											case 1:
												if (disposable != null)
												{
													num = 2;
													continue;
												}
												goto IL_244;
											case 2:
												disposable.Dispose();
												num = 0;
												continue;
											}
											break;
										}
									}
									IL_242:
									IL_244:;
								}
								goto IL_245;
							case 3:
							{
								IniSetting iniSetting;
								Control control = AppSettingsUtils.ᜀ(context, iniSetting.Name);
								Type type = control.GetType();
								IniSettings iniSettings = iniSetting;
								IEnumerator enumerator2 = iniSettings.GetEnumerator();
								num = 1;
								continue;
							}
							case 4:
							{
								IniSetting iniSetting;
								if (!(iniSetting.TypeName != HyperlinksCollectionEditor.b("次䨣伥笧伩堫娭夯就匳䔵", a_)))
								{
									num = 3;
									continue;
								}
								break;
							}
							case 5:
								goto IL_245;
							case 6:
								goto IL_251;
							}
							IL_AA:
							num = 0;
							continue;
							goto IL_AA;
							IL_245:
							num = 6;
						}
						IL_251:;
					}
					finally
					{
						for (;;)
						{
							IDisposable disposable2 = enumerator as IDisposable;
							int num = 2;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_2B5;
								case 1:
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										goto IL_27E;
									default:
										if (false)
										{
										}
										disposable2.Dispose();
										num = 0;
										continue;
									}
									break;
								case 2:
									if (disposable2 != null)
									{
										goto IL_27E;
									}
									goto IL_2B7;
								}
								break;
								IL_27E:
								num = 1;
							}
						}
						IL_2B5:
						IL_2B7:;
					}
					result = true;
					break;
				}
				}
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

		// Token: 0x06001134 RID: 4404 RVA: 0x000B8E20 File Offset: 0x000B7E20
		public static bool SaveAllPropertiesInBag(XMLSetting XmlXmlSetting, Control context)
		{
			int a_ = 11;
			bool result;
			try
			{
				switch (0)
				{
				default:
				{
					if (true)
					{
					}
					IniSection iniSection = XmlXmlSetting.Sections[HyperlinksCollectionEditor.b("眦嬨䐪崬䨮䌰䜲䰴甶堸尺", a_)];
					AppSettingsUtils.ᜀ(context);
					IEnumerator enumerator = iniSection.Settings.GetEnumerator();
					try
					{
						int num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								if (!enumerator.MoveNext())
								{
									num = 2;
									continue;
								}
								IniSetting iniSetting = (IniSetting)enumerator.Current;
								num = 5;
								continue;
							}
							case 2:
								goto IL_21C;
							case 3:
								goto IL_228;
							case 4:
								try
								{
									num = 0;
									for (;;)
									{
										switch (num)
										{
										case 1:
										{
											IEnumerator enumerator2;
											if (!enumerator2.MoveNext())
											{
												num = 2;
												continue;
											}
											IniSetting iniSetting2 = (IniSetting)enumerator2.Current;
											Type type;
											PropertyInfo property = type.GetProperty(iniSetting2.Name);
											num = 6;
											continue;
										}
										case 2:
											num = 3;
											continue;
										case 3:
											goto IL_1CE;
										case 4:
										{
											IniSetting iniSetting2;
											PropertyInfo property;
											Control control;
											iniSetting2.Value = property.GetValue(control, null);
											num = 5;
											continue;
										}
										case 6:
										{
											PropertyInfo property;
											if (property.CanRead)
											{
												num = 4;
												continue;
											}
											break;
										}
										}
										IL_19F:
										num = 1;
										continue;
										goto IL_19F;
									}
									IL_1CE:
									break;
								}
								finally
								{
									for (;;)
									{
										IEnumerator enumerator2;
										IDisposable disposable = enumerator2 as IDisposable;
										num = 0;
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
												goto IL_21B;
											case 1:
												disposable.Dispose();
												num = 2;
												continue;
											case 2:
												goto IL_219;
											}
											break;
										}
									}
									IL_219:
									IL_21B:;
								}
								goto IL_21C;
							case 5:
							{
								IniSetting iniSetting;
								if (!(iniSetting.TypeName != HyperlinksCollectionEditor.b("渦䜨䈪縬䨮䔰䜲尴夶常䠺", a_)))
								{
									num = 6;
									continue;
								}
								break;
							}
							case 6:
							{
								IniSetting iniSetting;
								Control control = AppSettingsUtils.ᜀ(context, iniSetting.Name);
								Type type = control.GetType();
								IniSettings iniSettings = iniSetting;
								IEnumerator enumerator2 = iniSettings.GetEnumerator();
								num = 4;
								continue;
							}
							}
							IL_B2:
							num = 0;
							continue;
							goto IL_B2;
							IL_21C:
							num = 3;
						}
						IL_228:;
					}
					finally
					{
						for (;;)
						{
							IDisposable disposable2 = enumerator as IDisposable;
							int num = 0;
							for (;;)
							{
								switch (num)
								{
								case 0:
									if (disposable2 != null)
									{
										goto IL_255;
									}
									goto IL_28E;
								case 1:
									goto IL_28C;
								case 2:
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										goto IL_255;
									default:
										if (false)
										{
										}
										disposable2.Dispose();
										num = 1;
										continue;
									}
									break;
								}
								break;
								IL_255:
								num = 2;
							}
						}
						IL_28C:
						IL_28E:;
					}
					result = true;
					break;
				}
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x000B9120 File Offset: 0x000B8120
		public static bool SaveItemsAndValues(ComboBox cbo, IniSection parentSection)
		{
			int a_ = 15;
			bool result;
			try
			{
				switch (0)
				{
				default:
					for (;;)
					{
						string text = cbo.Parent.Name + HyperlinksCollectionEditor.b("Ԫ", a_) + cbo.Name;
						IniSettings settings = parentSection.Settings;
						int num = 11;
						for (;;)
						{
							IniSettings iniSettings;
							int num2;
							int count;
							IniSettings iniSettings2;
							switch (num)
							{
							case 0:
								iniSettings = new IniSettings(parentSection.ᜄ);
								num = 4;
								continue;
							case 1:
								goto IL_144;
							case 2:
								goto IL_144;
							case 3:
								if (num2 >= count)
								{
									num = 7;
									continue;
								}
								iniSettings.Add(num2.ToString(), cbo.Items[num2]);
								num2++;
								num = 1;
								continue;
							case 4:
								goto IL_C2;
							case 5:
								goto IL_C2;
							case 6:
								goto IL_A3;
							case 7:
								iniSettings2.Add(HyperlinksCollectionEditor.b("截夬䨮尰䀲", a_), iniSettings);
								iniSettings2.Add(HyperlinksCollectionEditor.b("砪䠬䌮吰倲䄴制崸爺匼嬾⑀㭂", a_), cbo.SelectedIndex);
								settings.Add(text, iniSettings2);
								result = true;
								num = 10;
								continue;
							case 8:
								iniSettings2 = new IniSettings(parentSection.ᜄ);
								iniSettings = new IniSettings(parentSection.ᜄ);
								num = 6;
								continue;
							case 9:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_C2;
								default:
									if (false)
									{
									}
									goto IL_A3;
								}
								break;
							case 10:
								goto IL_25F;
							case 11:
								if (settings[text] == null)
								{
									num = 8;
									continue;
								}
								iniSettings2 = settings[text];
								num = 12;
								continue;
							case 12:
								if (settings[text][HyperlinksCollectionEditor.b("截夬䨮尰䀲", a_)] == null)
								{
									num = 0;
									continue;
								}
								iniSettings = settings[text][HyperlinksCollectionEditor.b("截夬䨮尰䀲", a_)];
								num = 5;
								continue;
							}
							break;
							IL_A3:
							count = cbo.Items.Count;
							num2 = 0;
							num = 2;
							continue;
							IL_C2:
							iniSettings2.Clear();
							iniSettings.Clear();
							num = 9;
							continue;
							IL_144:
							num = 3;
						}
					}
					IL_25F:
					break;
				}
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

		// Token: 0x06001136 RID: 4406 RVA: 0x000B93C4 File Offset: 0x000B83C4
		public static bool SaveItemsAndValues(ListBox lb, IniSection parentSection)
		{
			int a_ = 10;
			bool result;
			try
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_137:
					num = 2;
					break;
				default:
					if (false)
					{
					}
					switch (0)
					{
					default:
						goto IL_60;
					}
					break;
				}
				IniSettings settings;
				string text;
				for (;;)
				{
					IL_35:
					int num2;
					int count;
					switch (num)
					{
					case 0:
					{
						IniSettings iniSettings = new IniSettings(settings.ᜁ);
						settings.Add(text, iniSettings);
						num = 6;
						continue;
					}
					case 1:
						goto IL_E3;
					case 2:
						result = true;
						num = 4;
						continue;
					case 3:
						goto IL_124;
					case 4:
						goto IL_17A;
					case 5:
					{
						if (settings[text] == null)
						{
							num = 0;
							continue;
						}
						IniSettings iniSettings = settings[text];
						iniSettings.Clear();
						num = 1;
						continue;
					}
					case 6:
						goto IL_E3;
					case 7:
					{
						if (num2 >= count)
						{
							goto IL_137;
						}
						IniSettings iniSettings;
						iniSettings.Add(lb.Items[num2].ToString(), lb.GetSelected(num2));
						num2++;
						num = 3;
						continue;
					}
					case 8:
						goto IL_124;
					}
					goto IL_60;
					IL_E3:
					count = lb.Items.Count;
					num2 = 0;
					num = 8;
					continue;
					IL_124:
					num = 7;
				}
				IL_17A:
				goto IL_182;
				IL_60:
				text = lb.Parent.Name + HyperlinksCollectionEditor.b("ࠥ", a_) + lb.Name;
				settings = parentSection.Settings;
				num = 5;
				goto IL_35;
			}
			catch
			{
				result = false;
			}
			IL_182:
			if (true)
			{
			}
			return result;
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x000B9584 File Offset: 0x000B8584
		public static bool SaveItemsAndValues(TreeView tv, IniSection parentSection)
		{
			int a_ = 12;
			bool result;
			try
			{
				switch (0)
				{
				default:
					for (;;)
					{
						string text = tv.Parent.Name + HyperlinksCollectionEditor.b("ا", a_) + tv.Name;
						IniSettings settings = parentSection.Settings;
						int num = 4;
						for (;;)
						{
							IniSettings iniSettings;
							switch (num)
							{
							case 0:
								iniSettings = new IniSettings(parentSection.ᜄ);
								settings.Add(text, iniSettings);
								num = 3;
								continue;
							case 1:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_85;
								default:
									goto IL_FA;
								}
								break;
							case 2:
								goto IL_C9;
							case 3:
								goto IL_C9;
							case 4:
								if (settings[text] == null)
								{
									if (true)
									{
									}
									num = 0;
									continue;
								}
								goto IL_85;
							}
							break;
							IL_85:
							iniSettings = settings[text];
							iniSettings.Clear();
							num = 2;
							continue;
							IL_C9:
							AppSettingsUtils.ᜀ(tv.Nodes, ref iniSettings);
							result = true;
							num = 1;
						}
					}
					IL_FA:
					if (false)
					{
					}
					break;
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x000B96B4 File Offset: 0x000B86B4
		public static bool LoadItemsAndValues(ComboBox cbo, IniSection parentSection)
		{
			int a_ = 10;
			bool result;
			try
			{
				for (;;)
				{
					IL_09:
					switch (0)
					{
					default:
						for (;;)
						{
							string itemName = cbo.Parent.Name + HyperlinksCollectionEditor.b("ࠥ", a_) + cbo.Name;
							IniSettings iniSettings = parentSection.Settings[itemName];
							IniSettings iniSettings2 = iniSettings[HyperlinksCollectionEditor.b("漥尧伩䄫崭", a_)];
							int count = iniSettings2.Count;
							cbo.BeginUpdate();
							cbo.Items.Clear();
							int num = 0;
							int num2 = 1;
							for (;;)
							{
								switch (num2)
								{
								case 0:
								{
									int num3;
									if (num3 < count)
									{
										num2 = 3;
										continue;
									}
									goto IL_1AC;
								}
								case 1:
									goto IL_18B;
								case 2:
									goto IL_18B;
								case 3:
									num2 = 5;
									continue;
								case 4:
									if (num >= count)
									{
										num2 = 9;
										continue;
									}
									cbo.Items.Add(iniSettings2[num].Value);
									num++;
									num2 = 2;
									continue;
								case 5:
								{
									int num3;
									if (num3 > -2)
									{
										num2 = 6;
										continue;
									}
									goto IL_1AC;
								}
								case 6:
									if (true)
									{
									}
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										goto IL_09;
									default:
									{
										if (false)
										{
										}
										int num3;
										cbo.SelectedIndex = num3;
										num2 = 7;
										continue;
									}
									}
									break;
								case 7:
									goto IL_1AC;
								case 8:
									goto IL_1C1;
								case 9:
								{
									int num3 = iniSettings[HyperlinksCollectionEditor.b("甥䴧䘩䤫䴭䐯圱倳缵嘷帹夻䘽", a_)];
									num2 = 0;
									continue;
								}
								}
								break;
								IL_18B:
								num2 = 4;
								continue;
								IL_1AC:
								cbo.EndUpdate();
								result = true;
								num2 = 8;
							}
						}
						break;
					}
				}
				IL_1C1:;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x000B98B4 File Offset: 0x000B88B4
		public static bool LoadItemsAndValues(ListBox lb, IniSection parentSection)
		{
			int a_ = 19;
			bool result;
			try
			{
				switch (0)
				{
				default:
					for (;;)
					{
						string itemName = lb.Parent.Name + HyperlinksCollectionEditor.b("Į", a_) + lb.Name;
						IniSettings iniSettings = parentSection.Settings[itemName];
						int count = iniSettings.Count;
						lb.Items.Clear();
						lb.BeginUpdate();
						int num = 0;
						int num2 = 4;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								if (num >= count)
								{
									num2 = 2;
									continue;
								}
								for (;;)
								{
									int index = lb.Items.Add(iniSettings[num].Name);
									lb.SetSelected(index, iniSettings[num]);
									num++;
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										break;
									default:
										goto IL_F7;
									}
								}
								IL_F7:
								if (false)
								{
								}
								num2 = 1;
								continue;
							case 1:
								goto IL_92;
							case 2:
								lb.EndUpdate();
								result = true;
								num2 = 3;
								continue;
							case 3:
								goto IL_120;
							case 4:
								goto IL_92;
							}
							break;
							IL_92:
							num2 = 0;
						}
					}
					IL_120:
					break;
				}
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

		// Token: 0x0600113A RID: 4410 RVA: 0x000B9A1C File Offset: 0x000B8A1C
		public static bool LoadItemsAndValues(TreeView tv, IniSection parentSection)
		{
			int a_ = 0;
			bool result;
			try
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				string itemName = tv.Parent.Name + HyperlinksCollectionEditor.b("㈛", a_) + tv.Name;
				IniSettings a_2 = parentSection.Settings[itemName];
				tv.Nodes.Clear();
				tv.BeginUpdate();
				AppSettingsUtils.ᜀ(tv.Nodes, a_2);
				tv.EndUpdate();
				result = true;
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

		// Token: 0x0600113B RID: 4411 RVA: 0x000B9AD4 File Offset: 0x000B8AD4
		private static void ᜀ(TreeNodeCollection A_0, ref IniSettings A_1)
		{
			int a_ = 4;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_45;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			switch (0)
			{
			default:
			{
				IL_45:
				IEnumerator enumerator = A_0.GetEnumerator();
				try
				{
					int num = 9;
					for (;;)
					{
						string tag;
						switch (num)
						{
						case 0:
							goto IL_167;
						case 2:
						{
							TreeNode treeNode;
							tag = treeNode.Tag.ToString();
							num = 0;
							continue;
						}
						case 3:
							goto IL_1BA;
						case 4:
						{
							IniSettings iniSettings2;
							IniSettings iniSettings = iniSettings2.Add(HyperlinksCollectionEditor.b("挟䨡䴣䨥䰧堩䤫䀭", a_), new IniSettings(A_1.ᜁ));
							TreeNode treeNode;
							A_1.Add(treeNode.Text, iniSettings2, tag);
							AppSettingsUtils.ᜀ(treeNode.Nodes, ref iniSettings);
							num = 13;
							continue;
						}
						case 5:
						{
							if (!enumerator.MoveNext())
							{
								num = 17;
								continue;
							}
							TreeNode treeNode = (TreeNode)enumerator.Current;
							IniSettings iniSettings2 = new IniSettings(A_1.ᜁ);
							iniSettings2.Add(HyperlinksCollectionEditor.b("挟䨡䄣䔥䌧伩䠫", a_), treeNode.Checked);
							iniSettings2.Add(HyperlinksCollectionEditor.b("椟儡爣伥嬧䌩丫䈭唯", a_), treeNode.IsVisible);
							iniSettings2.Add(HyperlinksCollectionEditor.b("椟伡䔣䄥䴧挩䈫䨭唯䨱", a_), treeNode.ImageIndex);
							Color foreColor = treeNode.ForeColor;
							num = 14;
							continue;
						}
						case 6:
						{
							TreeNode treeNode;
							IniSettings iniSettings2;
							iniSettings2.Add(HyperlinksCollectionEditor.b("渟䴡䀣䌥渧䔩䈫娭", a_), treeNode.NodeFont);
							num = 7;
							continue;
						}
						case 7:
							goto IL_1BA;
						case 8:
						{
							TreeNode treeNode;
							if (treeNode.GetNodeCount(false) > 0)
							{
								num = 4;
								continue;
							}
							IniSettings iniSettings2;
							A_1.Add(treeNode.Text, iniSettings2, tag);
							num = 1;
							continue;
						}
						case 10:
							goto IL_1E2;
						case 11:
						{
							TreeNode treeNode;
							if (treeNode.NodeFont != null)
							{
								num = 6;
								continue;
							}
							IniSettings iniSettings2;
							iniSettings2.Add(HyperlinksCollectionEditor.b("渟䴡䀣䌥渧䔩䈫娭", a_), treeNode.TreeView.Font);
							num = 3;
							continue;
						}
						case 12:
						{
							TreeNode treeNode;
							if (treeNode.Tag != null)
							{
								num = 2;
								continue;
							}
							goto IL_167;
						}
						case 14:
						{
							Color foreColor;
							if (!foreColor.IsEmpty)
							{
								num = 16;
								continue;
							}
							TreeNode treeNode;
							IniSettings iniSettings2;
							iniSettings2.Add(HyperlinksCollectionEditor.b("星䴡嘣䌥欧䔩䀫䄭䈯", a_), treeNode.TreeView.ForeColor);
							num = 18;
							continue;
						}
						case 15:
							goto IL_359;
						case 16:
						{
							TreeNode treeNode;
							IniSettings iniSettings2;
							iniSettings2.Add(HyperlinksCollectionEditor.b("星䴡嘣䌥欧䔩䀫䄭䈯", a_), treeNode.ForeColor);
							num = 10;
							continue;
						}
						case 17:
							num = 15;
							continue;
						case 18:
							goto IL_1E2;
						}
						goto IL_AD;
						IL_167:
						num = 8;
						continue;
						IL_191:
						num = 5;
						continue;
						IL_AD:
						goto IL_191;
						IL_1BA:
						tag = "";
						num = 12;
						continue;
						IL_1E2:
						num = 11;
					}
					IL_359:;
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
									num = 2;
									continue;
								}
								goto IL_3A3;
							case 1:
								goto IL_3A1;
							case 2:
								disposable.Dispose();
								num = 1;
								continue;
							}
							break;
						}
					}
					IL_3A1:
					IL_3A3:;
				}
				return;
			}
			}
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x000B9EA4 File Offset: 0x000B8EA4
		private static void ᜀ(TreeNodeCollection A_0, IniSettings A_1)
		{
			int a_ = 17;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_3D;
			}
			if (false)
			{
			}
			switch (0)
			{
			default:
			{
				IL_3D:
				IEnumerator enumerator = A_1.GetEnumerator();
				try
				{
					int num = 8;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							IniSettings iniSettings;
							if (iniSettings[HyperlinksCollectionEditor.b("搬䈮倰吲倴縶圸强堼䜾", a_)] != null)
							{
								num = 12;
								continue;
							}
							goto IL_1D7;
						}
						case 1:
						{
							IniSettings iniSettings;
							TreeNode treeNode;
							AppSettingsUtils.ᜀ(treeNode.Nodes, iniSettings[HyperlinksCollectionEditor.b("測䜮堰弲儴䔶尸唺", a_)]);
							num = 14;
							continue;
						}
						case 2:
							goto IL_2FC;
						case 3:
						{
							IniSettings iniSettings;
							if (iniSettings[HyperlinksCollectionEditor.b("挬䀮唰嘲猴堶圸伺", a_)] != null)
							{
								num = 13;
								continue;
							}
							goto IL_20D;
						}
						case 4:
							goto IL_2BA;
						case 5:
						{
							IniSettings iniSettings;
							if (iniSettings[HyperlinksCollectionEditor.b("搬尮朰娲䘴帶嬸场堼", a_)])
							{
								num = 18;
								continue;
							}
							goto IL_A9;
						}
						case 6:
							num = 2;
							continue;
						case 7:
						{
							if (!enumerator.MoveNext())
							{
								num = 6;
								continue;
							}
							IniSetting iniSetting = (IniSetting)enumerator.Current;
							TreeNode treeNode = A_0.Add(iniSetting.Name);
							IniSettings iniSettings = iniSetting;
							treeNode.Checked = iniSettings[HyperlinksCollectionEditor.b("測䜮吰倲帴制崸", a_)];
							num = 5;
							continue;
						}
						case 9:
						{
							IniSettings iniSettings;
							if (iniSettings[HyperlinksCollectionEditor.b("欬䀮䌰嘲瘴堶唸吺似", a_)] != null)
							{
								num = 15;
								continue;
							}
							goto IL_2BA;
						}
						case 10:
							goto IL_20D;
						case 11:
							goto IL_1D7;
						case 12:
						{
							IniSettings iniSettings;
							TreeNode treeNode;
							treeNode.ImageIndex = iniSettings[HyperlinksCollectionEditor.b("搬䈮倰吲倴縶圸强堼䜾", a_)];
							num = 11;
							continue;
						}
						case 13:
						{
							IniSettings iniSettings;
							TreeNode treeNode;
							treeNode.NodeFont = iniSettings[HyperlinksCollectionEditor.b("挬䀮唰嘲猴堶圸伺", a_)];
							num = 10;
							continue;
						}
						case 15:
						{
							IniSettings iniSettings;
							TreeNode treeNode;
							treeNode.ForeColor = iniSettings[HyperlinksCollectionEditor.b("欬䀮䌰嘲瘴堶唸吺似", a_)];
							num = 4;
							continue;
						}
						case 16:
						{
							IniSettings iniSettings;
							if (iniSettings[HyperlinksCollectionEditor.b("測䜮堰弲儴䔶尸唺", a_)] != null)
							{
								num = 1;
								continue;
							}
							break;
						}
						case 17:
							goto IL_A9;
						case 18:
						{
							TreeNode treeNode;
							treeNode.EnsureVisible();
							num = 17;
							continue;
						}
						}
						goto IL_A4;
						IL_A9:
						num = 9;
						continue;
						IL_152:
						num = 7;
						continue;
						IL_A4:
						goto IL_152;
						IL_1D7:
						num = 16;
						continue;
						IL_20D:
						num = 0;
						continue;
						IL_2BA:
						num = 3;
					}
					IL_2FC:;
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
								goto IL_343;
							case 2:
								if (disposable != null)
								{
									num = 0;
									continue;
								}
								goto IL_345;
							}
							break;
						}
					}
					IL_343:
					IL_345:;
				}
				if (true)
				{
				}
				return;
			}
			}
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x000BA21C File Offset: 0x000B921C
		public static string CalcSHA512(string str)
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
			SHA512Managed sha512Managed = new SHA512Managed();
			Encoding.Default.GetByteCount(str);
			return Convert.ToBase64String(sha512Managed.ComputeHash(Encoding.Default.GetBytes(str)));
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x000BA280 File Offset: 0x000B9280
		public static string Encrypt(string str, string keyString)
		{
			int a_ = 0;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			byte[] bytes = Encoding.Default.GetBytes(str);
			SymmetricAlgorithm symmetricAlgorithm = SymmetricAlgorithm.Create(HyperlinksCollectionEditor.b("䠛氝䤟刡䠣䌥氧漩缫", a_));
			symmetricAlgorithm.Padding = PaddingMode.Zeros;
			symmetricAlgorithm.Key = Encoding.Default.GetBytes(AppSettingsUtils.ᜀ(keyString, symmetricAlgorithm));
			symmetricAlgorithm.GenerateIV();
			MemoryStream memoryStream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, symmetricAlgorithm.CreateEncryptor(), CryptoStreamMode.Write);
			cryptoStream.Write(bytes, 0, bytes.Length);
			cryptoStream.FlushFinalBlock();
			string text = Convert.ToBase64String(symmetricAlgorithm.IV);
			return text.Length.ToString(HyperlinksCollectionEditor.b("䐛Ⱍ", a_)) + text + Convert.ToBase64String(memoryStream.ToArray());
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x000BA36C File Offset: 0x000B936C
		public static string Decrypt(string str, string keyString)
		{
			int a_ = 19;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			int num = int.Parse(str.Substring(0, 2), NumberStyles.HexNumber);
			byte[] iv = Convert.FromBase64String(str.Substring(2, num));
			byte[] array = Convert.FromBase64String(str.Substring(2 + num));
			MemoryStream memoryStream = new MemoryStream();
			SymmetricAlgorithm symmetricAlgorithm = SymmetricAlgorithm.Create(HyperlinksCollectionEditor.b("笮䌰娲䔴嬶尸缺砼氾", a_));
			symmetricAlgorithm.Padding = PaddingMode.Zeros;
			symmetricAlgorithm.Key = Encoding.Default.GetBytes(AppSettingsUtils.ᜀ(keyString, symmetricAlgorithm));
			symmetricAlgorithm.IV = iv;
			CryptoStream cryptoStream = new CryptoStream(memoryStream, symmetricAlgorithm.CreateDecryptor(), CryptoStreamMode.Write);
			cryptoStream.Write(array, 0, array.Length);
			cryptoStream.FlushFinalBlock();
			symmetricAlgorithm.Clear();
			cryptoStream.Clear();
			return Encoding.Default.GetString(memoryStream.ToArray());
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x000BA468 File Offset: 0x000B9468
		private static string ᜀ(string A_0, SymmetricAlgorithm A_1)
		{
			string text;
			for (;;)
			{
				if (true)
				{
				}
				text = A_0;
				int num = Encoding.Default.GetByteCount(A_0) * 8;
				int num2 = 12;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						num2 = 3;
						continue;
					case 1:
						num2 = 10;
						continue;
					case 2:
						if (A_1.LegalKeySizes[0].SkipSize > 0)
						{
							num2 = 1;
							continue;
						}
						return text;
					case 3:
						if (num > A_1.LegalKeySizes[0].MaxSize)
						{
							num2 = 7;
							continue;
						}
						num2 = 4;
						continue;
					case 4:
						if (num < A_1.LegalKeySizes[0].MinSize)
						{
							num2 = 9;
							continue;
						}
						num2 = 2;
						continue;
					case 5:
					{
						int maxCharCount = Encoding.Default.GetMaxCharCount((A_1.LegalKeySizes[0].MaxSize - num) / 8);
						text = text.PadRight(text.Length + maxCharCount, 'X');
						num2 = 11;
						continue;
					}
					case 6:
						return text;
					case 7:
						goto IL_19B;
					case 8:
						return text;
					case 9:
						text = text.PadRight(text.Length + Encoding.Default.GetMaxCharCount((A_1.LegalKeySizes[0].MinSize - num) / 8));
						num2 = 8;
						continue;
					case 10:
						if (num % A_1.LegalKeySizes[0].SkipSize != 0)
						{
							num2 = 5;
							continue;
						}
						return text;
					case 11:
						return text;
					case 12:
						if (A_1.LegalKeySizes.Length <= 0)
						{
							return text;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_19B;
						default:
							if (false)
							{
							}
							num2 = 0;
							continue;
						}
						break;
					}
					break;
					IL_19B:
					text = text.Substring(0, Encoding.Default.GetMaxCharCount(A_1.LegalKeySizes[0].MaxSize / 8));
					num2 = 6;
				}
			}
			return text;
		}
	}
}
