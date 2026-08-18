using System;
using System.Collections.Generic;
using NLog.Config;
using NLog.LayoutRenderers;
using NLog.Layouts;

namespace NLog.Targets
{
	// Token: 0x02000149 RID: 329
	[Target("NLogViewer")]
	public class NLogViewerTarget : NetworkTarget
	{
		// Token: 0x06000BB9 RID: 3001 RVA: 0x0001B6C1 File Offset: 0x000198C1
		public NLogViewerTarget()
		{
			this.Parameters = new List<NLogViewerParameterInfo>();
			this.Renderer.Parameters = this.Parameters;
			base.NewLine = false;
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x0001B6F7 File Offset: 0x000198F7
		public NLogViewerTarget(string name) : this()
		{
			base.Name = name;
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x0001B706 File Offset: 0x00019906
		// (set) Token: 0x06000BBC RID: 3004 RVA: 0x0001B713 File Offset: 0x00019913
		public bool IncludeNLogData
		{
			get
			{
				return this.Renderer.IncludeNLogData;
			}
			set
			{
				this.Renderer.IncludeNLogData = value;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000BBD RID: 3005 RVA: 0x0001B721 File Offset: 0x00019921
		// (set) Token: 0x06000BBE RID: 3006 RVA: 0x0001B72E File Offset: 0x0001992E
		public string AppInfo
		{
			get
			{
				return this.Renderer.AppInfo;
			}
			set
			{
				this.Renderer.AppInfo = value;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000BBF RID: 3007 RVA: 0x0001B73C File Offset: 0x0001993C
		// (set) Token: 0x06000BC0 RID: 3008 RVA: 0x0001B749 File Offset: 0x00019949
		public bool IncludeCallSite
		{
			get
			{
				return this.Renderer.IncludeCallSite;
			}
			set
			{
				this.Renderer.IncludeCallSite = value;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000BC1 RID: 3009 RVA: 0x0001B757 File Offset: 0x00019957
		// (set) Token: 0x06000BC2 RID: 3010 RVA: 0x0001B764 File Offset: 0x00019964
		public bool IncludeSourceInfo
		{
			get
			{
				return this.Renderer.IncludeSourceInfo;
			}
			set
			{
				this.Renderer.IncludeSourceInfo = value;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000BC3 RID: 3011 RVA: 0x0001B772 File Offset: 0x00019972
		// (set) Token: 0x06000BC4 RID: 3012 RVA: 0x0001B77F File Offset: 0x0001997F
		public bool IncludeMdc
		{
			get
			{
				return this.Renderer.IncludeMdc;
			}
			set
			{
				this.Renderer.IncludeMdc = value;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000BC5 RID: 3013 RVA: 0x0001B78D File Offset: 0x0001998D
		// (set) Token: 0x06000BC6 RID: 3014 RVA: 0x0001B79A File Offset: 0x0001999A
		public bool IncludeNdc
		{
			get
			{
				return this.Renderer.IncludeNdc;
			}
			set
			{
				this.Renderer.IncludeNdc = value;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000BC7 RID: 3015 RVA: 0x0001B7A8 File Offset: 0x000199A8
		// (set) Token: 0x06000BC8 RID: 3016 RVA: 0x0001B7B5 File Offset: 0x000199B5
		public string NdcItemSeparator
		{
			get
			{
				return this.Renderer.NdcItemSeparator;
			}
			set
			{
				this.Renderer.NdcItemSeparator = value;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x0001B7C3 File Offset: 0x000199C3
		// (set) Token: 0x06000BCA RID: 3018 RVA: 0x0001B7CB File Offset: 0x000199CB
		[ArrayParameter(typeof(NLogViewerParameterInfo), "parameter")]
		public IList<NLogViewerParameterInfo> Parameters { get; private set; }

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000BCB RID: 3019 RVA: 0x0001B7D4 File Offset: 0x000199D4
		public Log4JXmlEventLayoutRenderer Renderer
		{
			get
			{
				return this.layout.Renderer;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x0001B7E1 File Offset: 0x000199E1
		// (set) Token: 0x06000BCD RID: 3021 RVA: 0x0001B7E9 File Offset: 0x000199E9
		public override Layout Layout
		{
			get
			{
				return this.layout;
			}
			set
			{
			}
		}

		// Token: 0x040002E0 RID: 736
		private readonly Log4JXmlEventLayout layout = new Log4JXmlEventLayout();
	}
}
