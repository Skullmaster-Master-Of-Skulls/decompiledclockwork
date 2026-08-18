using System;

namespace TechnoPro.Common.Public.Entities
{
	// Token: 0x020000EA RID: 234
	[Serializable]
	public abstract class BusinessBase<TU, TV> : BusinessBase<TU>
	{
		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x0000E7F7 File Offset: 0x0000C9F7
		// (set) Token: 0x0600056D RID: 1389 RVA: 0x0000E7FF File Offset: 0x0000C9FF
		public virtual TV SecondId { get; set; }

		// Token: 0x0600056E RID: 1390 RVA: 0x0000E808 File Offset: 0x0000CA08
		public override bool Equals(object obj)
		{
			return obj != null && obj.GetType() == base.GetType() && (this.MatchingIds((BusinessBase<TU, TV>)obj) || this.MatchingHashCodes(obj));
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0000E84C File Offset: 0x0000CA4C
		private bool MatchingIds(BusinessBase<TU, TV> obj)
		{
			TU id = this.Id;
			if (!id.Equals(default(TU)))
			{
				TV secondId = this.SecondId;
				if (secondId.Equals(default(TV)))
				{
					id = obj.Id;
					if (!id.Equals(default(TU)))
					{
						secondId = obj.SecondId;
						if (!secondId.Equals(default(TV)))
						{
							id = this.Id;
							if (id.Equals(obj.Id))
							{
								secondId = this.SecondId;
								return secondId.Equals(obj.SecondId);
							}
						}
					}
				}
			}
			return false;
		}
	}
}
