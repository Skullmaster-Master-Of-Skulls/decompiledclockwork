using System;
using System.Data;
using Spire.Xls.Core;

namespace Spire.Xls
{
	// Token: 0x02000048 RID: 72
	public class MarkerDesigner
	{
		// Token: 0x060004F6 RID: 1270 RVA: 0x00029454 File Offset: 0x00028454
		internal MarkerDesigner(IMarkersDesigner A_0)
		{
			this.ᜀ = A_0;
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00029470 File Offset: 0x00028470
		public void AddDataTable(string paraName, DataTable dataTable)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.ᜀ.AddVariable(paraName, dataTable);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x000294B8 File Offset: 0x000284B8
		public void AddDataView(string paraName, DataView dataView)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.ᜀ.AddVariable(paraName, dataView);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00029500 File Offset: 0x00028500
		public void AddArray(string paraName, object[] paramValues)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			this.ᜀ.AddVariable(paraName, paramValues);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00029548 File Offset: 0x00028548
		public void AddDataColumn(string paramName, DataColumn paramValue)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			this.ᜀ.AddVariable(paramName, paramValue);
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00029590 File Offset: 0x00028590
		public void AddParameter(string paraName, object paramValue)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.ᜀ.AddVariable(paraName, paramValue);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x000295D8 File Offset: 0x000285D8
		public void RemoveParameter(string paraName)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.ᜀ.RemoveVariable(paraName);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00029620 File Offset: 0x00028620
		public void Contains(string paramName)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.ᜀ.ContainsVariable(paramName);
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00029668 File Offset: 0x00028668
		public void Apply()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.ᜀ.ApplyMarkers();
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x000296B0 File Offset: 0x000286B0
		// (set) Token: 0x06000500 RID: 1280 RVA: 0x000296F8 File Offset: 0x000286F8
		public string Prefix
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ.MarkerPrefix;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜀ.MarkerPrefix = value;
			}
		}

		// Token: 0x040000DF RID: 223
		private long \u2609\u0098\u00AB\u00AE;

		// Token: 0x040000E0 RID: 224
		private float \u25D9\u00B0\u008F\u00A2;

		// Token: 0x040000E1 RID: 225
		private string \u2460\u00AC\u008A\u0094;

		// Token: 0x040000E2 RID: 226
		private IMarkersDesigner ᜀ;
	}
}
