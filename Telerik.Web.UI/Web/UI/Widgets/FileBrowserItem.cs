using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;

namespace Telerik.Web.UI.Widgets
{
	// Token: 0x0200132A RID: 4906
	public abstract class FileBrowserItem
	{
		// Token: 0x0600CCD1 RID: 52433
		public abstract void Serialize(StringWriter writer);

		// Token: 0x0600CCD2 RID: 52434 RVA: 0x002DA8A0 File Offset: 0x002D8AA0
		protected static void WriteJavascriptString(StringWriter writer, string s)
		{
			writer.Write("'");
			writer.Write(s.Replace("\\", "\\\\").Replace("'", "\\'").Replace("\n", "\\n").Replace("\r", "\\r"));
			writer.Write("'");
		}

		// Token: 0x0600CCD3 RID: 52435 RVA: 0x002DA906 File Offset: 0x002D8B06
		protected static void WriteSeparator(StringWriter writer)
		{
			writer.Write(",");
		}

		// Token: 0x0600CCD4 RID: 52436 RVA: 0x002DA914 File Offset: 0x002D8B14
		protected static void RemoveLastSeparator(StringWriter writer)
		{
			StringBuilder stringBuilder = writer.GetStringBuilder();
			stringBuilder.Remove(stringBuilder.Length - 1, 1);
		}

		// Token: 0x170041DE RID: 16862
		// (get) Token: 0x0600CCD5 RID: 52437 RVA: 0x002DA938 File Offset: 0x002D8B38
		public NameValueCollection Attributes
		{
			get
			{
				if (this._attributes == null)
				{
					this._attributes = new NameValueCollection();
				}
				return this._attributes;
			}
		}

		// Token: 0x170041DF RID: 16863
		// (get) Token: 0x0600CCD6 RID: 52438 RVA: 0x002DA953 File Offset: 0x002D8B53
		// (set) Token: 0x0600CCD7 RID: 52439 RVA: 0x002DA95B File Offset: 0x002D8B5B
		public virtual string Path { get; set; }

		// Token: 0x170041E0 RID: 16864
		// (get) Token: 0x0600CCD8 RID: 52440 RVA: 0x002DA964 File Offset: 0x002D8B64
		// (set) Token: 0x0600CCD9 RID: 52441 RVA: 0x002DA96C File Offset: 0x002D8B6C
		public virtual string Name { get; set; }

		// Token: 0x170041E1 RID: 16865
		// (get) Token: 0x0600CCDA RID: 52442 RVA: 0x002DA975 File Offset: 0x002D8B75
		// (set) Token: 0x0600CCDB RID: 52443 RVA: 0x002DA97D File Offset: 0x002D8B7D
		public virtual PathPermissions Permissions { get; set; }

		// Token: 0x170041E2 RID: 16866
		// (get) Token: 0x0600CCDC RID: 52444 RVA: 0x002DA986 File Offset: 0x002D8B86
		// (set) Token: 0x0600CCDD RID: 52445 RVA: 0x002DA98E File Offset: 0x002D8B8E
		public string Tag { get; set; }

		// Token: 0x0600CCDE RID: 52446 RVA: 0x002DA998 File Offset: 0x002D8B98
		public void SerializeAttributes(StringWriter writer)
		{
			writer.Write("{");
			if (this.Attributes.Count > 0)
			{
				foreach (object obj in this.Attributes.Keys)
				{
					string text = (string)obj;
					writer.Write("\"{0}\":\"{1}\",", text, this.Attributes[text]);
				}
				if (this.Attributes.Keys.Count > 0)
				{
					FileBrowserItem.RemoveLastSeparator(writer);
				}
			}
			writer.Write("}");
		}

		// Token: 0x0400369A RID: 13978
		private NameValueCollection _attributes;
	}
}
