using System;
using System.Drawing;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x0200106E RID: 4206
	public sealed class EditorColorCollection : StronglyTypedStateManagedCollection<EditorColor>
	{
		// Token: 0x0600A9A4 RID: 43428 RVA: 0x0024D83F File Offset: 0x0024BA3F
		internal EditorColorCollection()
		{
		}

		// Token: 0x0600A9A5 RID: 43429 RVA: 0x0024D847 File Offset: 0x0024BA47
		public void Add(string value)
		{
			this.Add(new EditorColor(Color.FromName(value)));
		}

		// Token: 0x0600A9A6 RID: 43430 RVA: 0x0024D85A File Offset: 0x0024BA5A
		protected override void SetDirtyObject(object o)
		{
			((StateManager)o).SetDirty();
		}

		// Token: 0x0600A9A7 RID: 43431 RVA: 0x0024D868 File Offset: 0x0024BA68
		internal string Serialize(JavaScriptSerializer serializer)
		{
			object[] array = new object[base.Count];
			for (int i = 0; i < base.Count; i++)
			{
				string text = ColorTranslator.ToHtml(this[i].Value);
				array[i] = text;
			}
			return serializer.Serialize(array);
		}
	}
}
