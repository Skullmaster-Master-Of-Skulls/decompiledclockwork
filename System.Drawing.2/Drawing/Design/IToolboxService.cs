using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;

namespace System.Drawing.Design
{
	// Token: 0x02000075 RID: 117
	[Guid("4BACD258-DE64-4048-BC4E-FEDBEF9ACB76")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IToolboxService
	{
		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000837 RID: 2103
		CategoryNameCollection CategoryNames { get; }

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000838 RID: 2104
		// (set) Token: 0x06000839 RID: 2105
		string SelectedCategory { get; set; }

		// Token: 0x0600083A RID: 2106
		void AddCreator(ToolboxItemCreatorCallback creator, string format);

		// Token: 0x0600083B RID: 2107
		void AddCreator(ToolboxItemCreatorCallback creator, string format, IDesignerHost host);

		// Token: 0x0600083C RID: 2108
		void AddLinkedToolboxItem(ToolboxItem toolboxItem, IDesignerHost host);

		// Token: 0x0600083D RID: 2109
		void AddLinkedToolboxItem(ToolboxItem toolboxItem, string category, IDesignerHost host);

		// Token: 0x0600083E RID: 2110
		void AddToolboxItem(ToolboxItem toolboxItem);

		// Token: 0x0600083F RID: 2111
		void AddToolboxItem(ToolboxItem toolboxItem, string category);

		// Token: 0x06000840 RID: 2112
		ToolboxItem DeserializeToolboxItem(object serializedObject);

		// Token: 0x06000841 RID: 2113
		ToolboxItem DeserializeToolboxItem(object serializedObject, IDesignerHost host);

		// Token: 0x06000842 RID: 2114
		ToolboxItem GetSelectedToolboxItem();

		// Token: 0x06000843 RID: 2115
		ToolboxItem GetSelectedToolboxItem(IDesignerHost host);

		// Token: 0x06000844 RID: 2116
		ToolboxItemCollection GetToolboxItems();

		// Token: 0x06000845 RID: 2117
		ToolboxItemCollection GetToolboxItems(IDesignerHost host);

		// Token: 0x06000846 RID: 2118
		ToolboxItemCollection GetToolboxItems(string category);

		// Token: 0x06000847 RID: 2119
		ToolboxItemCollection GetToolboxItems(string category, IDesignerHost host);

		// Token: 0x06000848 RID: 2120
		bool IsSupported(object serializedObject, IDesignerHost host);

		// Token: 0x06000849 RID: 2121
		bool IsSupported(object serializedObject, ICollection filterAttributes);

		// Token: 0x0600084A RID: 2122
		bool IsToolboxItem(object serializedObject);

		// Token: 0x0600084B RID: 2123
		bool IsToolboxItem(object serializedObject, IDesignerHost host);

		// Token: 0x0600084C RID: 2124
		void Refresh();

		// Token: 0x0600084D RID: 2125
		void RemoveCreator(string format);

		// Token: 0x0600084E RID: 2126
		void RemoveCreator(string format, IDesignerHost host);

		// Token: 0x0600084F RID: 2127
		void RemoveToolboxItem(ToolboxItem toolboxItem);

		// Token: 0x06000850 RID: 2128
		void RemoveToolboxItem(ToolboxItem toolboxItem, string category);

		// Token: 0x06000851 RID: 2129
		void SelectedToolboxItemUsed();

		// Token: 0x06000852 RID: 2130
		object SerializeToolboxItem(ToolboxItem toolboxItem);

		// Token: 0x06000853 RID: 2131
		bool SetCursor();

		// Token: 0x06000854 RID: 2132
		void SetSelectedToolboxItem(ToolboxItem toolboxItem);
	}
}
