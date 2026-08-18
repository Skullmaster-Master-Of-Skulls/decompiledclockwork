using System;
using System.ComponentModel;
using System.Globalization;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.XLS;

namespace Spire.DataExport.TypeConverters
{
	// Token: 0x020001A2 RID: 418
	public class CellValueTypeConverter : ExpandableObjectConverter
	{
		// Token: 0x06000B74 RID: 2932 RVA: 0x000792F4 File Offset: 0x000782F4
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == typeof(string))
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
					break;
				}
				return true;
			}
			return base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x00079348 File Offset: 0x00078348
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(string))
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
					break;
				}
				return true;
			}
			return base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x0007939C File Offset: 0x0007839C
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			int a_ = 19;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (context.Instance != null)
					{
						if (true)
						{
						}
						num = 1;
						continue;
					}
					goto IL_88;
				case 1:
					num = 7;
					continue;
				case 3:
					num = 0;
					continue;
				case 4:
					num = 5;
					continue;
				case 5:
					if (context != null)
					{
						num = 3;
						continue;
					}
					goto IL_88;
				case 6:
					goto IL_6E;
				case 7:
					if (context.Instance is Cell)
					{
						num = 6;
						continue;
					}
					goto IL_88;
				}
				if (!(value is string))
				{
					goto IL_88;
				}
				num = 4;
			}
			IL_6E:
			object result;
			try
			{
				for (;;)
				{
					CellType cellType = (context.Instance as Cell).CellType;
					num = 8;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_19B;
						case 1:
							goto IL_1DA;
						case 2:
							goto IL_1CD;
						case 3:
							goto IL_13F;
						case 4:
							num = 6;
							continue;
						case 5:
							goto IL_15B;
						case 6:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_10C;
							default:
								if (false)
								{
								}
								num = 1;
								continue;
							}
							break;
						case 7:
							goto IL_1B4;
						case 8:
							goto IL_10C;
						}
						break;
						IL_10C:
						switch (cellType)
						{
						case CellType.Boolean:
							result = Convert.ToBoolean(value);
							num = 2;
							break;
						case CellType.DateTime:
							result = Convert.ToDateTime(value);
							num = 5;
							break;
						case CellType.Numeric:
							result = Convert.ToDouble(value);
							num = 7;
							break;
						case CellType.String:
							result = Convert.ToString(value);
							num = 0;
							break;
						case CellType.Formula:
							result = Convert.ToString(value);
							num = 3;
							break;
						default:
							num = 4;
							break;
						}
					}
				}
				IL_13F:
				IL_15B:
				IL_19B:
				IL_1B4:
				IL_1CD:
				return result;
				IL_1DA:
				goto IL_88;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message + HyperlinksCollectionEditor.b("∮㬰瀲倴嬶唸洺尼匾㑀♂ᅄ㹆㥈⹊์⁎㽐╒ご╖ⵘ㹚⽜敞孠⁢੤०Ὠ๪Ὤ᭮㝰Ųᩴ᩶", a_));
			}
			return result;
			IL_88:
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x000795C8 File Offset: 0x000785C8
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			int a_ = 16;
			switch (0)
			{
			default:
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						num = 7;
						continue;
					case 2:
						num = 4;
						continue;
					case 3:
						goto IL_91;
					case 4:
						if (context.Instance != null)
						{
							num = 6;
							continue;
						}
						goto IL_B0;
					case 5:
						if (context.Instance is Cell)
						{
							num = 3;
							continue;
						}
						goto IL_B0;
					case 6:
						if (true)
						{
						}
						num = 5;
						continue;
					case 7:
						if (context != null)
						{
							num = 2;
							continue;
						}
						goto IL_B0;
					}
					if (destinationType != typeof(string))
					{
						goto IL_B0;
					}
					num = 1;
				}
				IL_91:
				object result;
				try
				{
					for (;;)
					{
						Cell cell = context.Instance as Cell;
						CultureInfo provider = new CultureInfo(cell.CultureName);
						CellType cellType = cell.CellType;
						num = 0;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_13F;
							case 1:
								goto IL_207;
							case 2:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_13F;
								default:
									if (false)
									{
									}
									num = 4;
									continue;
								}
								break;
							case 3:
								goto IL_1E2;
							case 4:
								goto IL_234;
							case 5:
								goto IL_178;
							case 6:
								goto IL_1A0;
							case 7:
								goto IL_226;
							case 8:
								num = 2;
								continue;
							}
							break;
							IL_13F:
							switch (cellType)
							{
							case CellType.Boolean:
								result = ((bool)value).ToString(provider);
								num = 7;
								break;
							case CellType.DateTime:
								result = ((DateTime)value).ToString(cell.DateTimeFormat, provider);
								num = 6;
								break;
							case CellType.Numeric:
								result = ((double)value).ToString(cell.NumericFormat, provider);
								num = 1;
								break;
							case CellType.String:
								result = value.ToString();
								num = 3;
								break;
							case CellType.Formula:
								result = value.ToString();
								num = 5;
								break;
							default:
								num = 8;
								break;
							}
						}
					}
					IL_178:
					IL_1A0:
					IL_1E2:
					IL_207:
					IL_226:
					return result;
					IL_234:
					goto IL_B0;
				}
				catch (Exception ex)
				{
					throw new Exception(ex.Message + HyperlinksCollectionEditor.b("Å␭猯圱堳娵渷嬹倻䬽┿ᙁ㵃㙅ⵇॉ⍋⁍♏㝑♓≕㵗⡙晛摝⍟ൡ੣ၥ൧ᡩᡫ㩭Ὧ", a_));
				}
				return result;
				IL_B0:
				return base.ConvertTo(context, culture, value, destinationType);
			}
			}
		}
	}
}
