using System;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Runtime.InteropServices;

namespace System.ComponentModel
{
	// Token: 0x0200055D RID: 1373
	[RootDesignerSerializer("System.ComponentModel.Design.Serialization.RootCodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", true)]
	[Designer("System.ComponentModel.Design.ComponentDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(IDesigner))]
	[Designer("System.Windows.Forms.Design.ComponentDocumentDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(IRootDesigner))]
	[TypeConverter(typeof(ComponentConverter))]
	[ComVisible(true)]
	public interface IComponent : IDisposable
	{
		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x0600338F RID: 13199
		// (set) Token: 0x06003390 RID: 13200
		ISite Site { get; set; }

		// Token: 0x1400004C RID: 76
		// (add) Token: 0x06003391 RID: 13201
		// (remove) Token: 0x06003392 RID: 13202
		event EventHandler Disposed;
	}
}
