using System;

namespace AutoComboBox.MyControls.CustomTableControls
{
	// Token: 0x020000A1 RID: 161
	public class ColumnTypeDefUtil
	{
		// Token: 0x0600062D RID: 1581 RVA: 0x000320D0 File Offset: 0x000310D0
		public static Type typeOf(ColumnTypeDefEnum e)
		{
			return ColumnTypeDefUtil.Types[(int)e];
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x000320EC File Offset: 0x000310EC
		public static ColumnTypeDef createInstance(ColumnTypeDefEnum e)
		{
			object obj = Activator.CreateInstance(ColumnTypeDefUtil.typeOf(e));
			return obj as ColumnTypeDef;
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00032110 File Offset: 0x00031110
		public static ColumnTypeDefEnum enumOf(Type t)
		{
			foreach (object obj in Enum.GetValues(typeof(ColumnTypeDefEnum)))
			{
				ColumnTypeDefEnum columnTypeDefEnum = (ColumnTypeDefEnum)obj;
				if (ColumnTypeDefUtil.Types[(int)columnTypeDefEnum].Equals(t))
				{
					return columnTypeDefEnum;
				}
			}
			throw new ArgumentException(t + " is not an valid type for me to verify");
		}

		// Token: 0x040004E8 RID: 1256
		public static readonly Type[] Types = new Type[]
		{
			typeof(DroplistDef),
			typeof(NotesDef),
			typeof(WhoenteredDef),
			typeof(DateDef),
			typeof(FileNameDef),
			typeof(CheckBoxDef)
		};

		// Token: 0x040004E9 RID: 1257
		public static readonly string[] StringRepresentationsOfTypes = new string[]
		{
			"Drop Down list",
			"Notes",
			"Group Enrolled",
			"Date",
			"File Name",
			"CheckBox"
		};
	}
}
