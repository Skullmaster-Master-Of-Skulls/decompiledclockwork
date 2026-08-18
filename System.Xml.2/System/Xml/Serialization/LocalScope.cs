using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace System.Xml.Serialization
{
	// Token: 0x02000135 RID: 309
	internal class LocalScope
	{
		// Token: 0x060016A1 RID: 5793 RVA: 0x00063DD1 File Offset: 0x00061FD1
		public LocalScope()
		{
			this.locals = new Dictionary<string, LocalBuilder>();
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x00063DE4 File Offset: 0x00061FE4
		public LocalScope(LocalScope parent) : this()
		{
			this.parent = parent;
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x00063DF3 File Offset: 0x00061FF3
		public void Add(string key, LocalBuilder value)
		{
			this.locals.Add(key, value);
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x00063E02 File Offset: 0x00062002
		public bool ContainsKey(string key)
		{
			return this.locals.ContainsKey(key) || (this.parent != null && this.parent.ContainsKey(key));
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x00063E2A File Offset: 0x0006202A
		public bool TryGetValue(string key, out LocalBuilder value)
		{
			if (this.locals.TryGetValue(key, out value))
			{
				return true;
			}
			if (this.parent != null)
			{
				return this.parent.TryGetValue(key, out value);
			}
			value = null;
			return false;
		}

		// Token: 0x170004A3 RID: 1187
		public LocalBuilder this[string key]
		{
			get
			{
				LocalBuilder result;
				this.TryGetValue(key, out result);
				return result;
			}
			set
			{
				this.locals[key] = value;
			}
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x00063E80 File Offset: 0x00062080
		public void AddToFreeLocals(Dictionary<Tuple<Type, string>, Queue<LocalBuilder>> freeLocals)
		{
			foreach (KeyValuePair<string, LocalBuilder> keyValuePair in this.locals)
			{
				Tuple<Type, string> key = new Tuple<Type, string>(keyValuePair.Value.LocalType, keyValuePair.Key);
				Queue<LocalBuilder> queue;
				if (freeLocals.TryGetValue(key, out queue))
				{
					queue.Enqueue(keyValuePair.Value);
				}
				else
				{
					queue = new Queue<LocalBuilder>();
					queue.Enqueue(keyValuePair.Value);
					freeLocals.Add(key, queue);
				}
			}
		}

		// Token: 0x04000A90 RID: 2704
		public readonly LocalScope parent;

		// Token: 0x04000A91 RID: 2705
		private readonly Dictionary<string, LocalBuilder> locals;
	}
}
