using System;
using System.Collections;

namespace System.Windows.Forms.Design.Behavior
{
	// Token: 0x02000370 RID: 880
	public sealed class BehaviorServiceAdornerCollection : CollectionBase
	{
		// Token: 0x06002401 RID: 9217 RVA: 0x000E058E File Offset: 0x000DE78E
		public BehaviorServiceAdornerCollection(BehaviorService behaviorService)
		{
			this.behaviorService = behaviorService;
		}

		// Token: 0x06002402 RID: 9218 RVA: 0x000E059D File Offset: 0x000DE79D
		public BehaviorServiceAdornerCollection(BehaviorServiceAdornerCollection value)
		{
			this.AddRange(value);
		}

		// Token: 0x06002403 RID: 9219 RVA: 0x000E05AC File Offset: 0x000DE7AC
		public BehaviorServiceAdornerCollection(Adorner[] value)
		{
			this.AddRange(value);
		}

		// Token: 0x170007A2 RID: 1954
		public Adorner this[int index]
		{
			get
			{
				return (Adorner)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x06002406 RID: 9222 RVA: 0x000E05CE File Offset: 0x000DE7CE
		public int Add(Adorner value)
		{
			value.BehaviorService = this.behaviorService;
			return base.List.Add(value);
		}

		// Token: 0x06002407 RID: 9223 RVA: 0x000E05E8 File Offset: 0x000DE7E8
		public void AddRange(Adorner[] value)
		{
			for (int i = 0; i < value.Length; i++)
			{
				this.Add(value[i]);
			}
		}

		// Token: 0x06002408 RID: 9224 RVA: 0x000E0610 File Offset: 0x000DE810
		public void AddRange(BehaviorServiceAdornerCollection value)
		{
			for (int i = 0; i < value.Count; i++)
			{
				this.Add(value[i]);
			}
		}

		// Token: 0x06002409 RID: 9225 RVA: 0x00057A39 File Offset: 0x00055C39
		public bool Contains(Adorner value)
		{
			return base.List.Contains(value);
		}

		// Token: 0x0600240A RID: 9226 RVA: 0x00057A55 File Offset: 0x00055C55
		public void CopyTo(Adorner[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x0600240B RID: 9227 RVA: 0x00057A2B File Offset: 0x00055C2B
		public int IndexOf(Adorner value)
		{
			return base.List.IndexOf(value);
		}

		// Token: 0x0600240C RID: 9228 RVA: 0x00057A1C File Offset: 0x00055C1C
		public void Insert(int index, Adorner value)
		{
			base.List.Insert(index, value);
		}

		// Token: 0x0600240D RID: 9229 RVA: 0x000E063C File Offset: 0x000DE83C
		public new BehaviorServiceAdornerCollectionEnumerator GetEnumerator()
		{
			return new BehaviorServiceAdornerCollectionEnumerator(this);
		}

		// Token: 0x0600240E RID: 9230 RVA: 0x00057A47 File Offset: 0x00055C47
		public void Remove(Adorner value)
		{
			base.List.Remove(value);
		}

		// Token: 0x04001A4B RID: 6731
		private BehaviorService behaviorService;
	}
}
