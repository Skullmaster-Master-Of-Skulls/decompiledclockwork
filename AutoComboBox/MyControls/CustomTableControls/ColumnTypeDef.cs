using System;
using System.Xml.Serialization;

namespace AutoComboBox.MyControls.CustomTableControls
{
	// Token: 0x020000A2 RID: 162
	[XmlInclude(typeof(WhoenteredDef))]
	[XmlInclude(typeof(DroplistDef))]
	[XmlInclude(typeof(NotesDef))]
	[XmlInclude(typeof(FileNameDef))]
	[XmlRoot(Namespace = null)]
	[XmlInclude(typeof(DateDef))]
	[XmlInclude(typeof(CheckBoxDef))]
	public abstract class ColumnTypeDef
	{
	}
}
