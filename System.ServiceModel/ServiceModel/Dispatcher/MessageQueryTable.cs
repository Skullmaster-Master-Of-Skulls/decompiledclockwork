using System;
using System.Collections;
using System.Collections.Generic;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000474 RID: 1140
	public class MessageQueryTable<TItem> : IDictionary<MessageQuery, TItem>, ICollection<KeyValuePair<MessageQuery, TItem>>, IEnumerable<KeyValuePair<MessageQuery, TItem>>, IEnumerable
	{
		// Token: 0x06002C4D RID: 11341 RVA: 0x000AD606 File Offset: 0x000AB806
		public MessageQueryTable()
		{
			this.dictionary = new Dictionary<MessageQuery, TItem>();
			this.collectionsByType = new Dictionary<Type, MessageQueryCollection>();
		}

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x06002C4E RID: 11342 RVA: 0x000AD624 File Offset: 0x000AB824
		public int Count
		{
			get
			{
				return this.dictionary.Count;
			}
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x06002C4F RID: 11343 RVA: 0x000AD631 File Offset: 0x000AB831
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x06002C50 RID: 11344 RVA: 0x000AD634 File Offset: 0x000AB834
		public ICollection<MessageQuery> Keys
		{
			get
			{
				return this.dictionary.Keys;
			}
		}

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x06002C51 RID: 11345 RVA: 0x000AD641 File Offset: 0x000AB841
		public ICollection<TItem> Values
		{
			get
			{
				return this.dictionary.Values;
			}
		}

		// Token: 0x17000AB2 RID: 2738
		public TItem this[MessageQuery key]
		{
			get
			{
				return this.dictionary[key];
			}
			set
			{
				this.Add(key, value);
			}
		}

		// Token: 0x06002C54 RID: 11348 RVA: 0x000AD668 File Offset: 0x000AB868
		public void Add(MessageQuery key, TItem value)
		{
			if (key == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("key");
			}
			Type type = key.GetType();
			MessageQueryCollection messageQueryCollection;
			if (!this.collectionsByType.TryGetValue(type, out messageQueryCollection))
			{
				messageQueryCollection = key.CreateMessageQueryCollection();
				if (messageQueryCollection == null)
				{
					messageQueryCollection = new MessageQueryTable<TItem>.SequentialMessageQueryCollection();
				}
				this.collectionsByType.Add(type, messageQueryCollection);
			}
			messageQueryCollection.Add(key);
			this.dictionary.Add(key, value);
		}

		// Token: 0x06002C55 RID: 11349 RVA: 0x000AD6D0 File Offset: 0x000AB8D0
		public void Add(KeyValuePair<MessageQuery, TItem> item)
		{
			this.Add(item.Key, item.Value);
		}

		// Token: 0x06002C56 RID: 11350 RVA: 0x000AD6E6 File Offset: 0x000AB8E6
		public void Clear()
		{
			this.collectionsByType.Clear();
			this.dictionary.Clear();
		}

		// Token: 0x06002C57 RID: 11351 RVA: 0x000AD6FE File Offset: 0x000AB8FE
		public bool Contains(KeyValuePair<MessageQuery, TItem> item)
		{
			return ((ICollection<KeyValuePair<MessageQuery, TItem>>)this.dictionary).Contains(item);
		}

		// Token: 0x06002C58 RID: 11352 RVA: 0x000AD70C File Offset: 0x000AB90C
		public bool ContainsKey(MessageQuery key)
		{
			return this.dictionary.ContainsKey(key);
		}

		// Token: 0x06002C59 RID: 11353 RVA: 0x000AD71A File Offset: 0x000AB91A
		public void CopyTo(KeyValuePair<MessageQuery, TItem>[] array, int arrayIndex)
		{
			((ICollection<KeyValuePair<MessageQuery, TItem>>)this.dictionary).CopyTo(array, arrayIndex);
		}

		// Token: 0x06002C5A RID: 11354 RVA: 0x000AD729 File Offset: 0x000AB929
		public IEnumerable<KeyValuePair<MessageQuery, TResult>> Evaluate<TResult>(Message message)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			return new MessageQueryTable<TItem>.MessageEnumerable<TResult>(this, message);
		}

		// Token: 0x06002C5B RID: 11355 RVA: 0x000AD745 File Offset: 0x000AB945
		public IEnumerable<KeyValuePair<MessageQuery, TResult>> Evaluate<TResult>(MessageBuffer buffer)
		{
			if (buffer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("buffer");
			}
			return new MessageQueryTable<TItem>.MessageBufferEnumerable<TResult>(this, buffer);
		}

		// Token: 0x06002C5C RID: 11356 RVA: 0x000AD761 File Offset: 0x000AB961
		public IEnumerator<KeyValuePair<MessageQuery, TItem>> GetEnumerator()
		{
			return ((IEnumerable<KeyValuePair<MessageQuery, TItem>>)this.dictionary).GetEnumerator();
		}

		// Token: 0x06002C5D RID: 11357 RVA: 0x000AD76E File Offset: 0x000AB96E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06002C5E RID: 11358 RVA: 0x000AD778 File Offset: 0x000AB978
		public bool Remove(MessageQuery key)
		{
			if (this.dictionary.Remove(key))
			{
				Type type = key.GetType();
				MessageQueryCollection messageQueryCollection = this.collectionsByType[type];
				messageQueryCollection.Remove(key);
				if (messageQueryCollection.Count == 0)
				{
					this.collectionsByType.Remove(type);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06002C5F RID: 11359 RVA: 0x000AD7C7 File Offset: 0x000AB9C7
		public bool Remove(KeyValuePair<MessageQuery, TItem> item)
		{
			return this.Remove(item.Key);
		}

		// Token: 0x06002C60 RID: 11360 RVA: 0x000AD7D6 File Offset: 0x000AB9D6
		public bool TryGetValue(MessageQuery key, out TItem value)
		{
			return this.dictionary.TryGetValue(key, out value);
		}

		// Token: 0x0400243E RID: 9278
		private Dictionary<Type, MessageQueryCollection> collectionsByType;

		// Token: 0x0400243F RID: 9279
		private Dictionary<MessageQuery, TItem> dictionary;

		// Token: 0x02000C43 RID: 3139
		private class SequentialMessageQueryCollection : MessageQueryCollection
		{
			// Token: 0x06007760 RID: 30560 RVA: 0x001BDE96 File Offset: 0x001BC096
			public override IEnumerable<KeyValuePair<MessageQuery, TResult>> Evaluate<TResult>(Message message)
			{
				return new MessageQueryTable<TItem>.SequentialMessageQueryCollection.MessageSequentialResultEnumerable<TResult>(this, message);
			}

			// Token: 0x06007761 RID: 30561 RVA: 0x001BDE9F File Offset: 0x001BC09F
			public override IEnumerable<KeyValuePair<MessageQuery, TResult>> Evaluate<TResult>(MessageBuffer buffer)
			{
				return new MessageQueryTable<TItem>.SequentialMessageQueryCollection.MessageBufferSequentialResultEnumerable<TResult>(this, buffer);
			}

			// Token: 0x02000F35 RID: 3893
			private abstract class SequentialResultEnumerable<TSource, TResult> : IEnumerable<KeyValuePair<MessageQuery, TResult>>, IEnumerable
			{
				// Token: 0x0600867F RID: 34431 RVA: 0x001F2A27 File Offset: 0x001F0C27
				public SequentialResultEnumerable(MessageQueryTable<TItem>.SequentialMessageQueryCollection collection, TSource source)
				{
					this.collection = collection;
					this.source = source;
				}

				// Token: 0x17001D82 RID: 7554
				// (get) Token: 0x06008680 RID: 34432 RVA: 0x001F2A3D File Offset: 0x001F0C3D
				private MessageQueryTable<TItem>.SequentialMessageQueryCollection Collection
				{
					get
					{
						return this.collection;
					}
				}

				// Token: 0x17001D83 RID: 7555
				// (get) Token: 0x06008681 RID: 34433 RVA: 0x001F2A45 File Offset: 0x001F0C45
				protected TSource Source
				{
					get
					{
						return this.source;
					}
				}

				// Token: 0x06008682 RID: 34434 RVA: 0x001F2A4D File Offset: 0x001F0C4D
				public IEnumerator<KeyValuePair<MessageQuery, TResult>> GetEnumerator()
				{
					return new MessageQueryTable<TItem>.SequentialMessageQueryCollection.SequentialResultEnumerable<TSource, TResult>.SequentialResultEnumerator(this);
				}

				// Token: 0x06008683 RID: 34435 RVA: 0x001F2A55 File Offset: 0x001F0C55
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x06008684 RID: 34436
				protected abstract TResult Evaluate(MessageQuery query);

				// Token: 0x04004E2D RID: 20013
				private MessageQueryTable<TItem>.SequentialMessageQueryCollection collection;

				// Token: 0x04004E2E RID: 20014
				private TSource source;

				// Token: 0x02000FC4 RID: 4036
				private class SequentialResultEnumerator : IEnumerator<KeyValuePair<MessageQuery, TResult>>, IDisposable, IEnumerator
				{
					// Token: 0x060088D3 RID: 35027 RVA: 0x001FDB3B File Offset: 0x001FBD3B
					public SequentialResultEnumerator(MessageQueryTable<TItem>.SequentialMessageQueryCollection.SequentialResultEnumerable<TSource, TResult> enumerable)
					{
						this.enumerable = enumerable;
						this.queries = enumerable.Collection.GetEnumerator();
					}

					// Token: 0x17001DB2 RID: 7602
					// (get) Token: 0x060088D4 RID: 35028 RVA: 0x001FDB5C File Offset: 0x001FBD5C
					public KeyValuePair<MessageQuery, TResult> Current
					{
						get
						{
							MessageQuery messageQuery = this.queries.Current;
							TResult value = this.enumerable.Evaluate(messageQuery);
							return new KeyValuePair<MessageQuery, TResult>(messageQuery, value);
						}
					}

					// Token: 0x17001DB3 RID: 7603
					// (get) Token: 0x060088D5 RID: 35029 RVA: 0x001FDB89 File Offset: 0x001FBD89
					object IEnumerator.Current
					{
						get
						{
							return this.Current;
						}
					}

					// Token: 0x060088D6 RID: 35030 RVA: 0x001FDB96 File Offset: 0x001FBD96
					public void Dispose()
					{
					}

					// Token: 0x060088D7 RID: 35031 RVA: 0x001FDB98 File Offset: 0x001FBD98
					public bool MoveNext()
					{
						return this.queries.MoveNext();
					}

					// Token: 0x060088D8 RID: 35032 RVA: 0x001FDBA5 File Offset: 0x001FBDA5
					public void Reset()
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
					}

					// Token: 0x04005073 RID: 20595
					private MessageQueryTable<TItem>.SequentialMessageQueryCollection.SequentialResultEnumerable<TSource, TResult> enumerable;

					// Token: 0x04005074 RID: 20596
					private IEnumerator<MessageQuery> queries;
				}
			}

			// Token: 0x02000F36 RID: 3894
			private class MessageSequentialResultEnumerable<TResult> : MessageQueryTable<TItem>.SequentialMessageQueryCollection.SequentialResultEnumerable<Message, TResult>
			{
				// Token: 0x06008685 RID: 34437 RVA: 0x001F2A5D File Offset: 0x001F0C5D
				public MessageSequentialResultEnumerable(MessageQueryTable<TItem>.SequentialMessageQueryCollection collection, Message message) : base(collection, message)
				{
				}

				// Token: 0x06008686 RID: 34438 RVA: 0x001F2A67 File Offset: 0x001F0C67
				protected override TResult Evaluate(MessageQuery query)
				{
					return query.Evaluate<TResult>(base.Source);
				}
			}

			// Token: 0x02000F37 RID: 3895
			private class MessageBufferSequentialResultEnumerable<TResult> : MessageQueryTable<TItem>.SequentialMessageQueryCollection.SequentialResultEnumerable<MessageBuffer, TResult>
			{
				// Token: 0x06008687 RID: 34439 RVA: 0x001F2A75 File Offset: 0x001F0C75
				public MessageBufferSequentialResultEnumerable(MessageQueryTable<TItem>.SequentialMessageQueryCollection collection, MessageBuffer buffer) : base(collection, buffer)
				{
				}

				// Token: 0x06008688 RID: 34440 RVA: 0x001F2A7F File Offset: 0x001F0C7F
				protected override TResult Evaluate(MessageQuery query)
				{
					return query.Evaluate<TResult>(base.Source);
				}
			}
		}

		// Token: 0x02000C44 RID: 3140
		private abstract class Enumerable<TSource, TResult> : IEnumerable<KeyValuePair<MessageQuery, TResult>>, IEnumerable
		{
			// Token: 0x06007763 RID: 30563 RVA: 0x001BDEB0 File Offset: 0x001BC0B0
			public Enumerable(MessageQueryTable<TItem> table, TSource source)
			{
				this.table = table;
				this.source = source;
			}

			// Token: 0x17001B4E RID: 6990
			// (get) Token: 0x06007764 RID: 30564 RVA: 0x001BDEC6 File Offset: 0x001BC0C6
			protected TSource Source
			{
				get
				{
					return this.source;
				}
			}

			// Token: 0x06007765 RID: 30565 RVA: 0x001BDECE File Offset: 0x001BC0CE
			public IEnumerator<KeyValuePair<MessageQuery, TResult>> GetEnumerator()
			{
				return new MessageQueryTable<TItem>.Enumerable<TSource, TResult>.Enumerator(this);
			}

			// Token: 0x06007766 RID: 30566
			protected abstract IEnumerator<KeyValuePair<MessageQuery, TResult>> GetInnerEnumerator(MessageQueryCollection collection);

			// Token: 0x06007767 RID: 30567 RVA: 0x001BDED6 File Offset: 0x001BC0D6
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0400444E RID: 17486
			private TSource source;

			// Token: 0x0400444F RID: 17487
			private MessageQueryTable<TItem> table;

			// Token: 0x02000F38 RID: 3896
			private class Enumerator : IEnumerator<KeyValuePair<MessageQuery, TResult>>, IDisposable, IEnumerator
			{
				// Token: 0x06008689 RID: 34441 RVA: 0x001F2A8D File Offset: 0x001F0C8D
				public Enumerator(MessageQueryTable<TItem>.Enumerable<TSource, TResult> enumerable)
				{
					this.outerEnumerator = enumerable.table.collectionsByType.Values.GetEnumerator();
					this.enumerable = enumerable;
				}

				// Token: 0x17001D84 RID: 7556
				// (get) Token: 0x0600868A RID: 34442 RVA: 0x001F2ABC File Offset: 0x001F0CBC
				public KeyValuePair<MessageQuery, TResult> Current
				{
					get
					{
						return this.innerEnumerator.Current;
					}
				}

				// Token: 0x17001D85 RID: 7557
				// (get) Token: 0x0600868B RID: 34443 RVA: 0x001F2AC9 File Offset: 0x001F0CC9
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x0600868C RID: 34444 RVA: 0x001F2AD6 File Offset: 0x001F0CD6
				public void Dispose()
				{
				}

				// Token: 0x0600868D RID: 34445 RVA: 0x001F2AD8 File Offset: 0x001F0CD8
				public bool MoveNext()
				{
					if (this.innerEnumerator != null && this.innerEnumerator.MoveNext())
					{
						return true;
					}
					if (!this.outerEnumerator.MoveNext())
					{
						return false;
					}
					MessageQueryCollection collection = this.outerEnumerator.Current;
					this.innerEnumerator = this.enumerable.GetInnerEnumerator(collection);
					return this.innerEnumerator.MoveNext();
				}

				// Token: 0x0600868E RID: 34446 RVA: 0x001F2B34 File Offset: 0x001F0D34
				public void Reset()
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
				}

				// Token: 0x04004E2F RID: 20015
				private MessageQueryTable<TItem>.Enumerable<TSource, TResult> enumerable;

				// Token: 0x04004E30 RID: 20016
				private IEnumerator<KeyValuePair<MessageQuery, TResult>> innerEnumerator;

				// Token: 0x04004E31 RID: 20017
				private IEnumerator<MessageQueryCollection> outerEnumerator;
			}
		}

		// Token: 0x02000C45 RID: 3141
		private class MessageBufferEnumerable<TResult> : MessageQueryTable<TItem>.Enumerable<MessageBuffer, TResult>
		{
			// Token: 0x06007768 RID: 30568 RVA: 0x001BDEDE File Offset: 0x001BC0DE
			public MessageBufferEnumerable(MessageQueryTable<TItem> table, MessageBuffer buffer) : base(table, buffer)
			{
			}

			// Token: 0x06007769 RID: 30569 RVA: 0x001BDEE8 File Offset: 0x001BC0E8
			protected override IEnumerator<KeyValuePair<MessageQuery, TResult>> GetInnerEnumerator(MessageQueryCollection collection)
			{
				return collection.Evaluate<TResult>(base.Source).GetEnumerator();
			}
		}

		// Token: 0x02000C46 RID: 3142
		private class MessageEnumerable<TResult> : MessageQueryTable<TItem>.Enumerable<Message, TResult>
		{
			// Token: 0x0600776A RID: 30570 RVA: 0x001BDEFB File Offset: 0x001BC0FB
			public MessageEnumerable(MessageQueryTable<TItem> table, Message message) : base(table, message)
			{
			}

			// Token: 0x0600776B RID: 30571 RVA: 0x001BDF05 File Offset: 0x001BC105
			protected override IEnumerator<KeyValuePair<MessageQuery, TResult>> GetInnerEnumerator(MessageQueryCollection collection)
			{
				return collection.Evaluate<TResult>(base.Source).GetEnumerator();
			}
		}
	}
}
