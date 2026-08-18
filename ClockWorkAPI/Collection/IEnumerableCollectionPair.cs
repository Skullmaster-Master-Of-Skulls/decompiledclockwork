using System;

namespace ClockWorkAPI.Collection
{
	// Token: 0x02000039 RID: 57
	public interface IEnumerableCollectionPair<T>
	{
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060002CE RID: 718
		IEnumerableCollection<INode<T>> Nodes { get; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060002CF RID: 719
		IEnumerableCollection<T> Values { get; }
	}
}
