using System;
using System.Collections;
using System.Collections.Generic;
using System.ServiceModel.Security;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009D3 RID: 2515
	[__DynamicallyInvokable]
	public sealed class MessageProperties : IDictionary<string, object>, ICollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable, IDisposable
	{
		// Token: 0x06006340 RID: 25408 RVA: 0x001727D4 File Offset: 0x001709D4
		[__DynamicallyInvokable]
		public MessageProperties()
		{
		}

		// Token: 0x06006341 RID: 25409 RVA: 0x001727DC File Offset: 0x001709DC
		[__DynamicallyInvokable]
		public MessageProperties(MessageProperties properties)
		{
			this.CopyProperties(properties);
		}

		// Token: 0x06006342 RID: 25410 RVA: 0x001727EB File Offset: 0x001709EB
		internal MessageProperties(KeyValuePair<string, object>[] array)
		{
			if (array == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("array"));
			}
			this.CopyProperties(array);
		}

		// Token: 0x06006343 RID: 25411 RVA: 0x00172812 File Offset: 0x00170A12
		private void ThrowDisposed()
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ObjectDisposedException(string.Empty, SR.GetString("ObjectDisposed", new object[]
			{
				base.GetType().ToString()
			})));
		}

		// Token: 0x170017EE RID: 6126
		[__DynamicallyInvokable]
		public object this[string name]
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.disposed)
				{
					this.ThrowDisposed();
				}
				object result;
				if (!this.TryGetValue(name, out result))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("MessagePropertyNotFound", new object[]
					{
						name
					})));
				}
				return result;
			}
			[__DynamicallyInvokable]
			set
			{
				if (this.disposed)
				{
					this.ThrowDisposed();
				}
				this.UpdateProperty(name, value, false);
			}
		}

		// Token: 0x170017EF RID: 6127
		// (get) Token: 0x06006346 RID: 25414 RVA: 0x001728AC File Offset: 0x00170AAC
		internal bool CanRecycle
		{
			get
			{
				return this.properties == null || this.properties.Length <= 8;
			}
		}

		// Token: 0x170017F0 RID: 6128
		// (get) Token: 0x06006347 RID: 25415 RVA: 0x001728C6 File Offset: 0x00170AC6
		[__DynamicallyInvokable]
		public int Count
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.disposed)
				{
					this.ThrowDisposed();
				}
				return this.propertyCount;
			}
		}

		// Token: 0x170017F1 RID: 6129
		// (get) Token: 0x06006348 RID: 25416 RVA: 0x001728DC File Offset: 0x00170ADC
		// (set) Token: 0x06006349 RID: 25417 RVA: 0x001728F2 File Offset: 0x00170AF2
		[__DynamicallyInvokable]
		public MessageEncoder Encoder
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.disposed)
				{
					this.ThrowDisposed();
				}
				return this.encoder;
			}
			[__DynamicallyInvokable]
			set
			{
				if (this.disposed)
				{
					this.ThrowDisposed();
				}
				this.AdjustPropertyCount(this.encoder == null, value == null);
				this.encoder = value;
			}
		}

		// Token: 0x170017F2 RID: 6130
		// (get) Token: 0x0600634A RID: 25418 RVA: 0x0017291C File Offset: 0x00170B1C
		// (set) Token: 0x0600634B RID: 25419 RVA: 0x00172939 File Offset: 0x00170B39
		[__DynamicallyInvokable]
		public bool AllowOutputBatching
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.disposed)
				{
					this.ThrowDisposed();
				}
				return this.allowOutputBatching == MessageProperties.trueBool;
			}
			[__DynamicallyInvokable]
			set
			{
				if (this.disposed)
				{
					this.ThrowDisposed();
				}
				this.AdjustPropertyCount(this.allowOutputBatching == null, false);
				if (value)
				{
					this.allowOutputBatching = MessageProperties.trueBool;
					return;
				}
				this.allowOutputBatching = MessageProperties.falseBool;
			}
		}

		// Token: 0x170017F3 RID: 6131
		// (get) Token: 0x0600634C RID: 25420 RVA: 0x00172973 File Offset: 0x00170B73
		[__DynamicallyInvokable]
		public bool IsFixedSize
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.disposed)
				{
					this.ThrowDisposed();
				}
				return false;
			}
		}

		// Token: 0x170017F4 RID: 6132
		// (get) Token: 0x0600634D RID: 25421 RVA: 0x00172984 File Offset: 0x00170B84
		public bool IsReadOnly
		{
			get
			{
				if (this.disposed)
				{
					this.ThrowDisposed();
				}
				return false;
			}
		}

		// Token: 0x170017F5 RID: 6133
		// (get) Token: 0x0600634E RID: 25422 RVA: 0x00172998 File Offset: 0x00170B98
		[__DynamicallyInvokable]
		public ICollection<string> Keys
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.disposed)
				{
					this.ThrowDisposed();
				}
				List<string> list = new List<string>();
				if (this.via != null)
				{
					list.Add("Via");
				}
				if (this.allowOutputBatching != null)
				{
					list.Add("AllowOutputBatching");
				}
				if (this.security != null)
				{
					list.Add("Security");
				}
				if (this.encoder != null)
				{
					list.Add("Encoder");
				}
				if (this.properties != null)
				{
					for (int i = 0; i < this.properties.Length; i++)
					{
						string name = this.properties[i].Name;
						if (name == null)
						{
							break;
						}
						list.Add(name);
					}
				}
				return list;
			}
		}

		// Token: 0x170017F6 RID: 6134
		// (get) Token: 0x0600634F RID: 25423 RVA: 0x00172A3D File Offset: 0x00170C3D
		// (set) Token: 0x06006350 RID: 25424 RVA: 0x00172A53 File Offset: 0x00170C53
		public SecurityMessageProperty Security
		{
			get
			{
				if (this.disposed)
				{
					this.ThrowDisposed();
				}
				return this.security;
			}
			set
			{
				if (this.disposed)
				{
					this.ThrowDisposed();
				}
				this.AdjustPropertyCount(this.security == null, value == null);
				this.security = value;
			}
		}

		// Token: 0x170017F7 RID: 6135
		// (get) Token: 0x06006351 RID: 25425 RVA: 0x00172A80 File Offset: 0x00170C80
		[__DynamicallyInvokable]
		public ICollection<object> Values
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.disposed)
				{
					this.ThrowDisposed();
				}
				List<object> list = new List<object>();
				if (this.via != null)
				{
					list.Add(this.via);
				}
				if (this.allowOutputBatching != null)
				{
					list.Add(this.allowOutputBatching);
				}
				if (this.security != null)
				{
					list.Add(this.security);
				}
				if (this.encoder != null)
				{
					list.Add(this.encoder);
				}
				if (this.properties != null)
				{
					int num = 0;
					while (num < this.properties.Length && this.properties[num].Name != null)
					{
						list.Add(this.properties[num].Value);
						num++;
					}
				}
				return list;
			}
		}

		// Token: 0x170017F8 RID: 6136
		// (get) Token: 0x06006352 RID: 25426 RVA: 0x00172B37 File Offset: 0x00170D37
		// (set) Token: 0x06006353 RID: 25427 RVA: 0x00172B4D File Offset: 0x00170D4D
		[__DynamicallyInvokable]
		public Uri Via
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.disposed)
				{
					this.ThrowDisposed();
				}
				return this.via;
			}
			[__DynamicallyInvokable]
			set
			{
				if (this.disposed)
				{
					this.ThrowDisposed();
				}
				this.AdjustPropertyCount(this.via == null, value == null);
				this.via = value;
			}
		}

		// Token: 0x06006354 RID: 25428 RVA: 0x00172B77 File Offset: 0x00170D77
		[__DynamicallyInvokable]
		public void Add(string name, object property)
		{
			if (this.disposed)
			{
				this.ThrowDisposed();
			}
			if (property == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("property"));
			}
			this.UpdateProperty(name, property, true);
		}

		// Token: 0x06006355 RID: 25429 RVA: 0x00172BA8 File Offset: 0x00170DA8
		private void AdjustPropertyCount(bool oldValueIsNull, bool newValueIsNull)
		{
			if (newValueIsNull)
			{
				if (!oldValueIsNull)
				{
					this.propertyCount--;
					return;
				}
			}
			else if (oldValueIsNull)
			{
				this.propertyCount++;
			}
		}

		// Token: 0x06006356 RID: 25430 RVA: 0x00172BD0 File Offset: 0x00170DD0
		[__DynamicallyInvokable]
		public void Clear()
		{
			if (this.disposed)
			{
				this.ThrowDisposed();
			}
			if (this.properties != null)
			{
				int num = 0;
				while (num < this.properties.Length && this.properties[num].Name != null)
				{
					this.properties[num] = default(MessageProperties.Property);
					num++;
				}
			}
			this.via = null;
			this.allowOutputBatching = null;
			this.security = null;
			this.encoder = null;
			this.propertyCount = 0;
		}

		// Token: 0x06006357 RID: 25431 RVA: 0x00172C50 File Offset: 0x00170E50
		[__DynamicallyInvokable]
		public void CopyProperties(MessageProperties properties)
		{
			if (properties == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("properties");
			}
			if (this.disposed)
			{
				this.ThrowDisposed();
			}
			if (properties.properties != null)
			{
				int num = 0;
				while (num < properties.properties.Length && properties.properties[num].Name != null)
				{
					MessageProperties.Property property = properties.properties[num];
					this[property.Name] = property.Value;
					num++;
				}
			}
			this.Via = properties.Via;
			this.AllowOutputBatching = properties.AllowOutputBatching;
			this.Security = ((properties.Security != null) ? ((SecurityMessageProperty)properties.Security.CreateCopy()) : null);
			this.Encoder = properties.Encoder;
		}

		// Token: 0x06006358 RID: 25432 RVA: 0x00172D14 File Offset: 0x00170F14
		internal void MergeProperties(MessageProperties properties)
		{
			if (properties == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("properties");
			}
			if (this.disposed)
			{
				this.ThrowDisposed();
			}
			if (properties.properties != null)
			{
				int num = 0;
				while (num < properties.properties.Length && properties.properties[num].Name != null)
				{
					MessageProperties.Property property = properties.properties[num];
					IMergeEnabledMessageProperty mergeEnabledMessageProperty;
					if (!this.TryGetValue<IMergeEnabledMessageProperty>(property.Name, out mergeEnabledMessageProperty) || !mergeEnabledMessageProperty.TryMergeWithProperty(property.Value))
					{
						this[property.Name] = property.Value;
					}
					num++;
				}
			}
			this.Via = properties.Via;
			this.AllowOutputBatching = properties.AllowOutputBatching;
			this.Security = ((properties.Security != null) ? ((SecurityMessageProperty)properties.Security.CreateCopy()) : null);
			this.Encoder = properties.Encoder;
		}

		// Token: 0x06006359 RID: 25433 RVA: 0x00172DF8 File Offset: 0x00170FF8
		internal void CopyProperties(KeyValuePair<string, object>[] array)
		{
			if (this.disposed)
			{
				this.ThrowDisposed();
			}
			foreach (KeyValuePair<string, object> keyValuePair in array)
			{
				this[keyValuePair.Key] = keyValuePair.Value;
			}
		}

		// Token: 0x0600635A RID: 25434 RVA: 0x00172E40 File Offset: 0x00171040
		[__DynamicallyInvokable]
		public bool ContainsKey(string name)
		{
			if (this.disposed)
			{
				this.ThrowDisposed();
			}
			if (name == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("name"));
			}
			switch (this.FindProperty(name))
			{
			case -5:
				return this.encoder != null;
			case -4:
				return this.security != null;
			case -3:
				return this.allowOutputBatching != null;
			case -2:
				return this.via != null;
			case -1:
				return false;
			default:
				return true;
			}
		}

		// Token: 0x0600635B RID: 25435 RVA: 0x00172EC8 File Offset: 0x001710C8
		private object CreateCopyOfPropertyValue(object propertyValue)
		{
			IMessageProperty messageProperty = propertyValue as IMessageProperty;
			if (messageProperty == null)
			{
				return propertyValue;
			}
			object obj = messageProperty.CreateCopy();
			if (obj == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("MessagePropertyReturnedNullCopy")));
			}
			return obj;
		}

		// Token: 0x0600635C RID: 25436 RVA: 0x00172F08 File Offset: 0x00171108
		[__DynamicallyInvokable]
		public void Dispose()
		{
			if (this.disposed)
			{
				return;
			}
			this.disposed = true;
			if (this.properties != null)
			{
				int num = 0;
				while (num < this.properties.Length && this.properties[num].Name != null)
				{
					this.properties[num].Dispose();
					num++;
				}
			}
			if (this.security != null)
			{
				this.security.Dispose();
			}
		}

		// Token: 0x0600635D RID: 25437 RVA: 0x00172F78 File Offset: 0x00171178
		private int FindProperty(string name)
		{
			if (name == "Via")
			{
				return -2;
			}
			if (name == "AllowOutputBatching")
			{
				return -3;
			}
			if (name == "Encoder")
			{
				return -5;
			}
			if (name == "Security")
			{
				return -4;
			}
			if (this.properties != null)
			{
				for (int i = 0; i < this.properties.Length; i++)
				{
					string name2 = this.properties[i].Name;
					if (name2 == null)
					{
						break;
					}
					if (name2 == name)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x0600635E RID: 25438 RVA: 0x00173001 File Offset: 0x00171201
		internal void Recycle()
		{
			this.disposed = false;
			this.Clear();
		}

		// Token: 0x0600635F RID: 25439 RVA: 0x00173010 File Offset: 0x00171210
		[__DynamicallyInvokable]
		public bool Remove(string name)
		{
			if (this.disposed)
			{
				this.ThrowDisposed();
			}
			int num = this.propertyCount;
			this.UpdateProperty(name, null, false);
			return num != this.propertyCount;
		}

		// Token: 0x06006360 RID: 25440 RVA: 0x00173048 File Offset: 0x00171248
		[__DynamicallyInvokable]
		public bool TryGetValue(string name, out object value)
		{
			if (this.disposed)
			{
				this.ThrowDisposed();
			}
			if (name == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("name"));
			}
			int num = this.FindProperty(name);
			switch (num)
			{
			case -5:
				value = this.encoder;
				break;
			case -4:
				value = this.security;
				break;
			case -3:
				value = this.allowOutputBatching;
				break;
			case -2:
				value = this.via;
				break;
			case -1:
				value = null;
				break;
			default:
				value = this.properties[num].Value;
				break;
			}
			return value != null;
		}

		// Token: 0x06006361 RID: 25441 RVA: 0x001730E8 File Offset: 0x001712E8
		internal bool TryGetValue<TProperty>(string name, out TProperty property)
		{
			object obj;
			if (this.TryGetValue(name, out obj))
			{
				property = (TProperty)((object)obj);
				return true;
			}
			property = default(TProperty);
			return false;
		}

		// Token: 0x06006362 RID: 25442 RVA: 0x00173116 File Offset: 0x00171316
		internal TProperty GetValue<TProperty>(string name) where TProperty : class
		{
			return this.GetValue<TProperty>(name, false);
		}

		// Token: 0x06006363 RID: 25443 RVA: 0x00173120 File Offset: 0x00171320
		internal TProperty GetValue<TProperty>(string name, bool ensureTypeMatch) where TProperty : class
		{
			object obj;
			if (!this.TryGetValue(name, out obj))
			{
				return default(TProperty);
			}
			if (!ensureTypeMatch)
			{
				return obj as TProperty;
			}
			return (TProperty)((object)obj);
		}

		// Token: 0x06006364 RID: 25444 RVA: 0x00173158 File Offset: 0x00171358
		private void UpdateProperty(string name, object value, bool mustNotExist)
		{
			if (name == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("name"));
			}
			int num = this.FindProperty(name);
			if (num == -1)
			{
				if (value != null)
				{
					int num2;
					if (this.properties == null)
					{
						this.properties = new MessageProperties.Property[2];
						num2 = 0;
					}
					else
					{
						num2 = 0;
						while (num2 < this.properties.Length && this.properties[num2].Name != null)
						{
							num2++;
						}
						if (num2 == this.properties.Length)
						{
							MessageProperties.Property[] destinationArray = new MessageProperties.Property[this.properties.Length * 2];
							Array.Copy(this.properties, destinationArray, this.properties.Length);
							this.properties = destinationArray;
						}
					}
					object value2 = this.CreateCopyOfPropertyValue(value);
					this.properties[num2] = new MessageProperties.Property(name, value2);
					this.propertyCount++;
				}
				return;
			}
			if (mustNotExist)
			{
				bool flag;
				switch (num)
				{
				case -5:
					flag = (this.encoder != null);
					break;
				case -4:
					flag = (this.security != null);
					break;
				case -3:
					flag = (this.allowOutputBatching != null);
					break;
				case -2:
					flag = (this.via != null);
					break;
				default:
					flag = true;
					break;
				}
				if (flag)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("DuplicateMessageProperty", new object[]
					{
						name
					})));
				}
			}
			if (num >= 0)
			{
				if (value == null)
				{
					this.properties[num].Dispose();
					int num3 = num + 1;
					while (num3 < this.properties.Length && this.properties[num3].Name != null)
					{
						this.properties[num3 - 1] = this.properties[num3];
						num3++;
					}
					this.properties[num3 - 1] = default(MessageProperties.Property);
					this.propertyCount--;
					return;
				}
				this.properties[num].Value = this.CreateCopyOfPropertyValue(value);
				return;
			}
			else
			{
				switch (num)
				{
				case -5:
					this.Encoder = (MessageEncoder)value;
					return;
				case -4:
					if (this.Security != null)
					{
						this.Security.Dispose();
					}
					this.Security = (SecurityMessageProperty)this.CreateCopyOfPropertyValue(value);
					return;
				case -3:
					this.AllowOutputBatching = (bool)value;
					return;
				case -2:
					this.Via = (Uri)value;
					return;
				default:
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException());
				}
			}
		}

		// Token: 0x06006365 RID: 25445 RVA: 0x001733BC File Offset: 0x001715BC
		[__DynamicallyInvokable]
		void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int index)
		{
			if (this.disposed)
			{
				this.ThrowDisposed();
			}
			if (array == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("array"));
			}
			if (array.Length < this.propertyCount)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("MessagePropertiesArraySize0")));
			}
			if (index < 0 || index > array.Length - this.propertyCount)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("index", index, SR.GetString("ValueMustBeInRange", new object[]
				{
					0,
					array.Length - this.propertyCount
				})));
			}
			if (this.via != null)
			{
				array[index++] = new KeyValuePair<string, object>("Via", this.via);
			}
			if (this.allowOutputBatching != null)
			{
				array[index++] = new KeyValuePair<string, object>("AllowOutputBatching", this.allowOutputBatching);
			}
			if (this.security != null)
			{
				array[index++] = new KeyValuePair<string, object>("Security", this.security.CreateCopy());
			}
			if (this.encoder != null)
			{
				array[index++] = new KeyValuePair<string, object>("Encoder", this.encoder);
			}
			if (this.properties != null)
			{
				for (int i = 0; i < this.properties.Length; i++)
				{
					string name = this.properties[i].Name;
					if (name == null)
					{
						break;
					}
					array[index++] = new KeyValuePair<string, object>(name, this.CreateCopyOfPropertyValue(this.properties[i].Value));
				}
			}
		}

		// Token: 0x06006366 RID: 25446 RVA: 0x00173560 File Offset: 0x00171760
		[__DynamicallyInvokable]
		void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> pair)
		{
			if (this.disposed)
			{
				this.ThrowDisposed();
			}
			if (pair.Value == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("pair.Value"));
			}
			this.UpdateProperty(pair.Key, pair.Value, true);
		}

		// Token: 0x06006367 RID: 25447 RVA: 0x001735B0 File Offset: 0x001717B0
		[__DynamicallyInvokable]
		bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> pair)
		{
			if (this.disposed)
			{
				this.ThrowDisposed();
			}
			if (pair.Value == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("pair.Value"));
			}
			if (pair.Key == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("pair.Key"));
			}
			object obj;
			return this.TryGetValue(pair.Key, out obj) && obj.Equals(pair.Value);
		}

		// Token: 0x06006368 RID: 25448 RVA: 0x00173627 File Offset: 0x00171827
		[__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			if (this.disposed)
			{
				this.ThrowDisposed();
			}
			return ((IEnumerable<KeyValuePair<string, object>>)this).GetEnumerator();
		}

		// Token: 0x06006369 RID: 25449 RVA: 0x00173640 File Offset: 0x00171840
		[__DynamicallyInvokable]
		IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
		{
			if (this.disposed)
			{
				this.ThrowDisposed();
			}
			List<KeyValuePair<string, object>> list = new List<KeyValuePair<string, object>>(this.propertyCount);
			if (this.via != null)
			{
				list.Add(new KeyValuePair<string, object>("Via", this.via));
			}
			if (this.allowOutputBatching != null)
			{
				list.Add(new KeyValuePair<string, object>("AllowOutputBatching", this.allowOutputBatching));
			}
			if (this.security != null)
			{
				list.Add(new KeyValuePair<string, object>("Security", this.security));
			}
			if (this.encoder != null)
			{
				list.Add(new KeyValuePair<string, object>("Encoder", this.encoder));
			}
			if (this.properties != null)
			{
				for (int i = 0; i < this.properties.Length; i++)
				{
					string name = this.properties[i].Name;
					if (name == null)
					{
						break;
					}
					list.Add(new KeyValuePair<string, object>(name, this.properties[i].Value));
				}
			}
			return list.GetEnumerator();
		}

		// Token: 0x0600636A RID: 25450 RVA: 0x00173740 File Offset: 0x00171940
		[__DynamicallyInvokable]
		bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> pair)
		{
			if (this.disposed)
			{
				this.ThrowDisposed();
			}
			if (pair.Value == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("pair.Value"));
			}
			if (pair.Key == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("pair.Key"));
			}
			object obj;
			if (!this.TryGetValue(pair.Key, out obj))
			{
				return false;
			}
			if (!obj.Equals(pair.Value))
			{
				return false;
			}
			this.Remove(pair.Key);
			return true;
		}

		// Token: 0x0400394E RID: 14670
		private MessageProperties.Property[] properties;

		// Token: 0x0400394F RID: 14671
		private int propertyCount;

		// Token: 0x04003950 RID: 14672
		private MessageEncoder encoder;

		// Token: 0x04003951 RID: 14673
		private Uri via;

		// Token: 0x04003952 RID: 14674
		private object allowOutputBatching;

		// Token: 0x04003953 RID: 14675
		private SecurityMessageProperty security;

		// Token: 0x04003954 RID: 14676
		private bool disposed;

		// Token: 0x04003955 RID: 14677
		private const int InitialPropertyCount = 2;

		// Token: 0x04003956 RID: 14678
		private const int MaxRecycledArrayLength = 8;

		// Token: 0x04003957 RID: 14679
		private const string ViaKey = "Via";

		// Token: 0x04003958 RID: 14680
		private const string AllowOutputBatchingKey = "AllowOutputBatching";

		// Token: 0x04003959 RID: 14681
		private const string SecurityKey = "Security";

		// Token: 0x0400395A RID: 14682
		private const string EncoderKey = "Encoder";

		// Token: 0x0400395B RID: 14683
		private const int NotFoundIndex = -1;

		// Token: 0x0400395C RID: 14684
		private const int ViaIndex = -2;

		// Token: 0x0400395D RID: 14685
		private const int AllowOutputBatchingIndex = -3;

		// Token: 0x0400395E RID: 14686
		private const int SecurityIndex = -4;

		// Token: 0x0400395F RID: 14687
		private const int EncoderIndex = -5;

		// Token: 0x04003960 RID: 14688
		private static object trueBool = true;

		// Token: 0x04003961 RID: 14689
		private static object falseBool = false;

		// Token: 0x02000E4C RID: 3660
		private struct Property : IDisposable
		{
			// Token: 0x060082DF RID: 33503 RVA: 0x001E3AFB File Offset: 0x001E1CFB
			public Property(string name, object value)
			{
				this.name = name;
				this.value = value;
			}

			// Token: 0x17001CF2 RID: 7410
			// (get) Token: 0x060082E0 RID: 33504 RVA: 0x001E3B0B File Offset: 0x001E1D0B
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x17001CF3 RID: 7411
			// (get) Token: 0x060082E1 RID: 33505 RVA: 0x001E3B13 File Offset: 0x001E1D13
			// (set) Token: 0x060082E2 RID: 33506 RVA: 0x001E3B1B File Offset: 0x001E1D1B
			public object Value
			{
				get
				{
					return this.value;
				}
				set
				{
					this.value = value;
				}
			}

			// Token: 0x060082E3 RID: 33507 RVA: 0x001E3B24 File Offset: 0x001E1D24
			public void Dispose()
			{
				IDisposable disposable = this.value as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}

			// Token: 0x04004A5F RID: 19039
			private string name;

			// Token: 0x04004A60 RID: 19040
			private object value;
		}
	}
}
