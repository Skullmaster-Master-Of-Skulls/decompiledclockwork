using System;
using System.Collections.Generic;
using Telerik.Web.UI.ImageEditor;

namespace Telerik.Web.UI
{
	// Token: 0x02000BAA RID: 2986
	public class ImageEditorEditingEventArgs : ImageEditorEventArgs
	{
		// Token: 0x06007084 RID: 28804 RVA: 0x001A4327 File Offset: 0x001A2527
		public ImageEditorEditingEventArgs(EditableImage image) : this(image, string.Empty)
		{
		}

		// Token: 0x06007085 RID: 28805 RVA: 0x001A4335 File Offset: 0x001A2535
		public ImageEditorEditingEventArgs(EditableImage image, string commandName) : this(image, commandName, string.Empty)
		{
		}

		// Token: 0x06007086 RID: 28806 RVA: 0x001A4344 File Offset: 0x001A2544
		public ImageEditorEditingEventArgs(EditableImage image, string commandName, string argument) : base(image)
		{
			this.CommandName = commandName;
			this.Argument = argument;
		}

		// Token: 0x06007087 RID: 28807 RVA: 0x001A435B File Offset: 0x001A255B
		public ImageEditorEditingEventArgs(EditableImage image, string commandName, string argument, Dictionary<string, object> clientDictionary) : this(image, commandName, argument)
		{
			this.ClientObjectsDictionary = clientDictionary;
		}

		// Token: 0x170024C9 RID: 9417
		// (get) Token: 0x06007088 RID: 28808 RVA: 0x001A436E File Offset: 0x001A256E
		// (set) Token: 0x06007089 RID: 28809 RVA: 0x001A4376 File Offset: 0x001A2576
		public bool Cancel
		{
			get
			{
				return this._cancel;
			}
			set
			{
				this._cancel = value;
			}
		}

		// Token: 0x170024CA RID: 9418
		// (get) Token: 0x0600708A RID: 28810 RVA: 0x001A437F File Offset: 0x001A257F
		// (set) Token: 0x0600708B RID: 28811 RVA: 0x001A4387 File Offset: 0x001A2587
		public string Argument { get; set; }

		// Token: 0x170024CB RID: 9419
		// (get) Token: 0x0600708C RID: 28812 RVA: 0x001A4390 File Offset: 0x001A2590
		// (set) Token: 0x0600708D RID: 28813 RVA: 0x001A4398 File Offset: 0x001A2598
		public string CommandName { get; private set; }

		// Token: 0x170024CC RID: 9420
		// (get) Token: 0x0600708E RID: 28814 RVA: 0x001A43A1 File Offset: 0x001A25A1
		// (set) Token: 0x0600708F RID: 28815 RVA: 0x001A43A9 File Offset: 0x001A25A9
		public Dictionary<string, object> ClientObjectsDictionary { get; private set; }

		// Token: 0x04001E68 RID: 7784
		private bool _cancel;
	}
}
