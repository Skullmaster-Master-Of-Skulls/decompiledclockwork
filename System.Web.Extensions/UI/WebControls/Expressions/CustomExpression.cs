using System;
using System.Linq;

namespace System.Web.UI.WebControls.Expressions
{
	// Token: 0x020000C9 RID: 201
	[PersistChildren(false)]
	[ParseChildren(true, "Parameters")]
	public class CustomExpression : ParameterDataSourceExpression
	{
		// Token: 0x14000045 RID: 69
		// (add) Token: 0x060009FC RID: 2556 RVA: 0x00025E91 File Offset: 0x00024091
		// (remove) Token: 0x060009FD RID: 2557 RVA: 0x00025EAA File Offset: 0x000240AA
		public event EventHandler<CustomExpressionEventArgs> Querying
		{
			add
			{
				this._querying = (EventHandler<CustomExpressionEventArgs>)Delegate.Combine(this._querying, value);
			}
			remove
			{
				this._querying = (EventHandler<CustomExpressionEventArgs>)Delegate.Remove(this._querying, value);
			}
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x00025EC4 File Offset: 0x000240C4
		public override IQueryable GetQueryable(IQueryable source)
		{
			CustomExpressionEventArgs customExpressionEventArgs = new CustomExpressionEventArgs(source, this.GetValues());
			this.OnQuerying(customExpressionEventArgs);
			return customExpressionEventArgs.Query;
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x00025EEB File Offset: 0x000240EB
		private void OnQuerying(CustomExpressionEventArgs e)
		{
			if (this._querying != null)
			{
				this._querying(this, e);
			}
		}

		// Token: 0x04000342 RID: 834
		private EventHandler<CustomExpressionEventArgs> _querying;
	}
}
