using System;
using System.Collections.Specialized;
using System.Configuration.Install;
using System.Data;
using ClockWorkWebAPI;

namespace ClockWorkWebAPIWeb
{
	// Token: 0x0200000B RID: 11
	public class DynamicControl
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00005E28 File Offset: 0x00004028
		public bool HasSpecialInstructions
		{
			get
			{
				return this.controlGroup.Length > 0;
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00005E48 File Offset: 0x00004048
		public string SpecialInstructions(string key)
		{
			bool flag = this.specialInstructionArgs != null;
			string result;
			if (flag)
			{
				result = this.specialInstructionArgs[key];
			}
			else
			{
				bool flag2 = this.controlGroup != null && this.controlGroup.Length > 0;
				if (flag2)
				{
					this.specialInstructionArgs = DynamicControl.ParseArgs(this.controlGroup);
					result = this.specialInstructionArgs[key];
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00005EB8 File Offset: 0x000040B8
		public static StringDictionary ParseArgs(string args)
		{
			InstallContext installContext = new InstallContext(null, args.Split("\r\n".ToCharArray()));
			return installContext.Parameters;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00005EEC File Offset: 0x000040EC
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00005F04 File Offset: 0x00004104
		public bool WebHiddenField
		{
			get
			{
				return this.webHiddenField;
			}
			set
			{
				this.webHiddenField = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00005F10 File Offset: 0x00004110
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00005F28 File Offset: 0x00004128
		public int ControlId
		{
			get
			{
				return this.control_id;
			}
			set
			{
				this.control_id = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00005F34 File Offset: 0x00004134
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00005F4C File Offset: 0x0000414C
		public object ControlValue
		{
			get
			{
				return this.controlValue;
			}
			set
			{
				this.controlValue = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00005F58 File Offset: 0x00004158
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00005F70 File Offset: 0x00004170
		public string ControlCaption
		{
			get
			{
				return this.control_caption;
			}
			set
			{
				this.control_caption = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00005F7C File Offset: 0x0000417C
		public string FrenchControlCaptionForDisplay
		{
			get
			{
				int num = this.setting4String.IndexOf("__");
				bool flag = num >= 0;
				string result;
				if (flag)
				{
					result = ((num == 0) ? "" : this.setting4String.Substring(0, num));
				}
				else
				{
					num = this.setting4String.IndexOf("~~");
					bool flag2 = num >= 0;
					if (flag2)
					{
						result = ((num == 0) ? "" : this.setting4String.Substring(0, num));
					}
					else
					{
						result = this.setting4String;
					}
				}
				return result;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00006004 File Offset: 0x00004204
		public string ControlCaptionForDisplay
		{
			get
			{
				int num = this.control_caption.IndexOf("__");
				bool flag = num >= 0;
				string result;
				if (flag)
				{
					result = ((num == 0) ? "" : this.control_caption.Substring(0, num));
				}
				else
				{
					num = this.control_caption.IndexOf("~~");
					bool flag2 = num >= 0;
					if (flag2)
					{
						result = ((num == 0) ? "" : this.control_caption.Substring(0, num));
					}
					else
					{
						result = this.control_caption;
					}
				}
				return result;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600007C RID: 124 RVA: 0x0000608C File Offset: 0x0000428C
		public int ControlCode
		{
			get
			{
				return this.control_code;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600007D RID: 125 RVA: 0x000060A4 File Offset: 0x000042A4
		// (set) Token: 0x0600007E RID: 126 RVA: 0x000060BC File Offset: 0x000042BC
		public int Setting1
		{
			get
			{
				return this.setting1;
			}
			set
			{
				this.setting1 = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600007F RID: 127 RVA: 0x000060C8 File Offset: 0x000042C8
		// (set) Token: 0x06000080 RID: 128 RVA: 0x000060E0 File Offset: 0x000042E0
		public int Setting2
		{
			get
			{
				return this.setting2;
			}
			set
			{
				this.setting2 = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000081 RID: 129 RVA: 0x000060EC File Offset: 0x000042EC
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00006104 File Offset: 0x00004304
		public int Setting3
		{
			get
			{
				return this.setting3;
			}
			set
			{
				this.setting3 = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00006110 File Offset: 0x00004310
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00006128 File Offset: 0x00004328
		public int DefaultValue
		{
			get
			{
				return this.default_value;
			}
			set
			{
				this.default_value = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00006134 File Offset: 0x00004334
		// (set) Token: 0x06000086 RID: 134 RVA: 0x0000614C File Offset: 0x0000434C
		public string ControlName
		{
			get
			{
				return this.controlName;
			}
			set
			{
				this.controlName = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00006158 File Offset: 0x00004358
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00006170 File Offset: 0x00004370
		public string ControlGroup
		{
			get
			{
				return this.controlGroup;
			}
			set
			{
				this.controlGroup = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000089 RID: 137 RVA: 0x0000617C File Offset: 0x0000437C
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00006194 File Offset: 0x00004394
		public string HelpText
		{
			get
			{
				return this.helpText;
			}
			set
			{
				this.helpText = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000061A0 File Offset: 0x000043A0
		// (set) Token: 0x0600008C RID: 140 RVA: 0x000061B8 File Offset: 0x000043B8
		public string Mask
		{
			get
			{
				return this.mask;
			}
			set
			{
				this.mask = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000061C4 File Offset: 0x000043C4
		public string ActionHandlers
		{
			get
			{
				return this.actionHandlers;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600008E RID: 142 RVA: 0x000061DC File Offset: 0x000043DC
		// (set) Token: 0x0600008F RID: 143 RVA: 0x000061F4 File Offset: 0x000043F4
		public string DefaultValueString
		{
			get
			{
				return this.defaultValueString;
			}
			set
			{
				this.defaultValueString = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00006200 File Offset: 0x00004400
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00006218 File Offset: 0x00004418
		public string Setting4String
		{
			get
			{
				return this.setting4String;
			}
			set
			{
				this.setting4String = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00006224 File Offset: 0x00004424
		public int HelpTextDisplayMethod
		{
			get
			{
				return this.helpTextDisplayMethod;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000093 RID: 147 RVA: 0x0000623C File Offset: 0x0000443C
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00006254 File Offset: 0x00004454
		public int Setting4
		{
			get
			{
				return this.setting4;
			}
			set
			{
				this.setting4 = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00006260 File Offset: 0x00004460
		public int FontSize
		{
			get
			{
				return this.fontSize;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00006278 File Offset: 0x00004478
		// (set) Token: 0x06000097 RID: 151 RVA: 0x00006290 File Offset: 0x00004490
		public int Enforce
		{
			get
			{
				return this.enforce;
			}
			set
			{
				this.enforce = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000098 RID: 152 RVA: 0x0000629C File Offset: 0x0000449C
		// (set) Token: 0x06000099 RID: 153 RVA: 0x000062B4 File Offset: 0x000044B4
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				this.enabled = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600009A RID: 154 RVA: 0x000062C0 File Offset: 0x000044C0
		// (set) Token: 0x0600009B RID: 155 RVA: 0x000062D8 File Offset: 0x000044D8
		public bool ReadOnly
		{
			get
			{
				return this.readOnly;
			}
			set
			{
				this.readOnly = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600009C RID: 156 RVA: 0x000062E4 File Offset: 0x000044E4
		// (set) Token: 0x0600009D RID: 157 RVA: 0x000062FC File Offset: 0x000044FC
		public bool HideCaption
		{
			get
			{
				return this.hideCaption;
			}
			set
			{
				this.hideCaption = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00006308 File Offset: 0x00004508
		// (set) Token: 0x0600009F RID: 159 RVA: 0x00006320 File Offset: 0x00004520
		public bool DontWrapToNextLine
		{
			get
			{
				return this.dontWrapToNextLine;
			}
			set
			{
				this.dontWrapToNextLine = value;
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000632C File Offset: 0x0000452C
		public DynamicControl(DataRow dr, DynamicControlLayoutHelper helper)
		{
			bool flag = dr != null && dr.RowState != DataRowState.Deleted;
			if (flag)
			{
				this.control_id = ((dr["controlid"] == DBNull.Value) ? -1 : ((int)dr["controlid"]));
				this.control_caption = dr["controlcaption"].ToString();
				this.control_code = (int)dr["controlcode"];
				this.setting1 = (int)dr["setting1"];
				this.setting2 = (int)dr["setting2"];
				this.setting3 = (int)dr["setting3"];
				this.default_value = (int)dr["defaultvalue"];
				DataTable table = dr.Table;
				string[] array = new string[]
				{
					"controlname",
					"controlgroup",
					"helptext",
					"mask",
					"actionhandlers",
					"defaultvaluestring",
					"setting4string"
				};
				foreach (string text in array)
				{
					bool flag2 = table.Columns.Contains(text) && dr[text] == DBNull.Value;
					if (flag2)
					{
						dr[text] = "";
					}
				}
				string[] array3 = new string[]
				{
					"setting4",
					"fontsize",
					"enforce"
				};
				foreach (string text2 in array3)
				{
					bool flag3 = table.Columns.Contains(text2) && dr[text2] == DBNull.Value;
					if (flag3)
					{
						dr[text2] = 0;
					}
				}
				bool flag4 = table.Columns.Contains("helptextdisplaymethod") && dr["helptextdisplaymethod"] == DBNull.Value;
				if (flag4)
				{
					dr["helptextdisplaymethod"] = 1;
				}
				string[] array5 = new string[]
				{
					"readonly",
					"hidecaption",
					"dontwraptonextline"
				};
				foreach (string text3 in array5)
				{
					bool flag5 = table.Columns.Contains(text3) && dr[text3] == DBNull.Value;
					if (flag5)
					{
						dr[text3] = false;
					}
				}
				bool flag6 = table.Columns.Contains("enabled") && dr["enabled"] == DBNull.Value;
				if (flag6)
				{
					dr["enabled"] = true;
				}
				bool flag7 = table.Columns.Contains("controlname");
				if (flag7)
				{
					this.controlName = (string)dr["controlname"];
				}
				else
				{
					this.controlName = "";
				}
				bool flag8 = table.Columns.Contains("controlgroup");
				if (flag8)
				{
					this.controlGroup = (string)dr["controlgroup"];
				}
				else
				{
					this.controlGroup = "";
				}
				bool flag9 = table.Columns.Contains("helptext");
				if (flag9)
				{
					this.helpText = (string)dr["helptext"];
				}
				else
				{
					this.helpText = "";
				}
				bool flag10 = table.Columns.Contains("mask");
				if (flag10)
				{
					this.mask = (string)dr["mask"];
				}
				else
				{
					this.mask = "";
				}
				bool flag11 = table.Columns.Contains("actionhandlers");
				if (flag11)
				{
					this.actionHandlers = (string)dr["actionhandlers"];
				}
				else
				{
					this.actionHandlers = "";
				}
				bool flag12 = table.Columns.Contains("defaultvaluestring");
				if (flag12)
				{
					this.defaultValueString = (string)dr["defaultvaluestring"];
				}
				else
				{
					this.defaultValueString = "";
				}
				bool flag13 = table.Columns.Contains("setting4string");
				if (flag13)
				{
					this.setting4String = (string)dr["setting4string"];
				}
				else
				{
					this.setting4String = "";
				}
				bool flag14 = table.Columns.Contains("helptextdisplaymethod");
				if (flag14)
				{
					this.helpTextDisplayMethod = (int)dr["helptextdisplaymethod"];
				}
				else
				{
					this.helpTextDisplayMethod = 1;
				}
				bool flag15 = table.Columns.Contains("setting4");
				if (flag15)
				{
					this.setting4 = (int)dr["setting4"];
				}
				else
				{
					this.setting4 = 0;
				}
				bool flag16 = table.Columns.Contains("fontsize");
				if (flag16)
				{
					this.fontSize = (int)dr["fontsize"];
				}
				else
				{
					this.fontSize = 0;
				}
				bool flag17 = table.Columns.Contains("enforce");
				if (flag17)
				{
					this.enforce = (int)dr["enforce"];
				}
				else
				{
					this.enforce = 0;
				}
				bool flag18 = table.Columns.Contains("enabled");
				if (flag18)
				{
					this.enabled = Convert.ToBoolean(dr["enabled"]);
				}
				else
				{
					this.enabled = true;
				}
				bool flag19 = table.Columns.Contains("readonly");
				if (flag19)
				{
					this.readOnly = Convert.ToBoolean(dr["readonly"]);
				}
				else
				{
					this.readOnly = false;
				}
				bool flag20 = table.Columns.Contains("hidecaption");
				if (flag20)
				{
					this.hideCaption = Convert.ToBoolean(dr["hidecaption"]);
				}
				else
				{
					this.hideCaption = false;
				}
				bool flag21 = table.Columns.Contains("dontwraptonextline");
				if (flag21)
				{
					this.dontWrapToNextLine = Convert.ToBoolean(dr["dontwraptonextline"]);
				}
				else
				{
					this.dontWrapToNextLine = false;
				}
				bool flag22 = table.Columns.Contains("controlvalueint");
				if (flag22)
				{
					this.controlValue = DynamicControl.GetControlValue(this, dr, helper);
				}
			}
			else
			{
				this.control_id = -1;
				this.control_caption = "";
				this.control_code = -1;
				this.setting1 = -1;
				this.setting2 = -1;
				this.setting3 = -1;
				this.default_value = -1;
				this.controlValue = null;
				this.SetDefaultExtendedValues();
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000069BC File Offset: 0x00004BBC
		public static object GetControlValue(DynamicControl dc, DataRow dr, DynamicControlLayoutHelper helper)
		{
			object result;
			switch (dc.ControlCode)
			{
			case 1:
			{
				bool flag = dr["controlvaluebytes"] == DBNull.Value;
				if (flag)
				{
					result = null;
				}
				else
				{
					result = Core.BytesToString((byte[])dr["controlvaluebytes"], dc.setting3 == 1, helper.Conn.TripleDES);
				}
				break;
			}
			case 2:
				result = (dr["controlvalueint"] != DBNull.Value && Convert.ToBoolean(dr["controlvalueint"]));
				break;
			case 3:
			{
				bool flag2 = dc.Setting3 == 0;
				if (flag2)
				{
					bool flag3 = dr["controlvalueint"] == DBNull.Value;
					if (flag3)
					{
						result = null;
					}
					else
					{
						result = null;
					}
				}
				else
				{
					bool flag4 = dr["controlvaluebytes"] == DBNull.Value;
					if (flag4)
					{
						result = null;
					}
					else
					{
						result = Core.BytesToString((byte[])dr["controlvaluebytes"], dc.setting3 == -1, helper.Conn.TripleDES);
					}
				}
				break;
			}
			default:
				result = null;
				break;
			}
			return result;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00006AEC File Offset: 0x00004CEC
		private void SetDefaultExtendedValues()
		{
			this.controlName = "";
			this.controlGroup = "";
			this.helpText = "";
			this.mask = "";
			this.actionHandlers = "";
			this.defaultValueString = "";
			this.setting4String = "";
			this.helpTextDisplayMethod = 1;
			this.setting4 = 0;
			this.fontSize = 0;
			this.enforce = 0;
			this.enabled = true;
			this.readOnly = false;
			this.hideCaption = false;
			this.dontWrapToNextLine = false;
		}

		// Token: 0x0400001A RID: 26
		private StringDictionary specialInstructionArgs = null;

		// Token: 0x0400001B RID: 27
		private bool webHiddenField = false;

		// Token: 0x0400001C RID: 28
		private int control_id;

		// Token: 0x0400001D RID: 29
		private string control_caption;

		// Token: 0x0400001E RID: 30
		private int control_code;

		// Token: 0x0400001F RID: 31
		private int setting1;

		// Token: 0x04000020 RID: 32
		private int setting2;

		// Token: 0x04000021 RID: 33
		private int setting3;

		// Token: 0x04000022 RID: 34
		private int default_value;

		// Token: 0x04000023 RID: 35
		private string controlName;

		// Token: 0x04000024 RID: 36
		private string controlGroup;

		// Token: 0x04000025 RID: 37
		private string helpText;

		// Token: 0x04000026 RID: 38
		private string mask;

		// Token: 0x04000027 RID: 39
		private string actionHandlers;

		// Token: 0x04000028 RID: 40
		private string defaultValueString;

		// Token: 0x04000029 RID: 41
		private string setting4String;

		// Token: 0x0400002A RID: 42
		private int helpTextDisplayMethod;

		// Token: 0x0400002B RID: 43
		private int setting4;

		// Token: 0x0400002C RID: 44
		private int fontSize;

		// Token: 0x0400002D RID: 45
		private int enforce;

		// Token: 0x0400002E RID: 46
		private bool enabled;

		// Token: 0x0400002F RID: 47
		private bool readOnly;

		// Token: 0x04000030 RID: 48
		private bool hideCaption;

		// Token: 0x04000031 RID: 49
		private bool dontWrapToNextLine;

		// Token: 0x04000032 RID: 50
		private object controlValue;
	}
}
