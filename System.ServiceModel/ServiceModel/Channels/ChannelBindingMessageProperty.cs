using System;
using System.Security.Authentication.ExtendedProtection;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007C4 RID: 1988
	internal sealed class ChannelBindingMessageProperty : IDisposable, IMessageProperty
	{
		// Token: 0x06004AF2 RID: 19186 RVA: 0x00112BE6 File Offset: 0x00110DE6
		public ChannelBindingMessageProperty(ChannelBinding channelBinding, bool ownsCleanup)
		{
			if (channelBinding == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("channelBinding");
			}
			this.refCount = 1;
			this.thisLock = new object();
			this.channelBinding = channelBinding;
			this.ownsCleanup = ownsCleanup;
		}

		// Token: 0x170012D7 RID: 4823
		// (get) Token: 0x06004AF3 RID: 19187 RVA: 0x00112C21 File Offset: 0x00110E21
		public static string Name
		{
			get
			{
				return "ChannelBindingMessageProperty";
			}
		}

		// Token: 0x170012D8 RID: 4824
		// (get) Token: 0x06004AF4 RID: 19188 RVA: 0x00112C28 File Offset: 0x00110E28
		private bool IsDisposed
		{
			get
			{
				return this.refCount <= 0;
			}
		}

		// Token: 0x170012D9 RID: 4825
		// (get) Token: 0x06004AF5 RID: 19189 RVA: 0x00112C36 File Offset: 0x00110E36
		public ChannelBinding ChannelBinding
		{
			get
			{
				this.ThrowIfDisposed();
				return this.channelBinding;
			}
		}

		// Token: 0x06004AF6 RID: 19190 RVA: 0x00112C44 File Offset: 0x00110E44
		public static bool TryGet(Message message, out ChannelBindingMessageProperty property)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			return ChannelBindingMessageProperty.TryGet(message.Properties, out property);
		}

		// Token: 0x06004AF7 RID: 19191 RVA: 0x00112C68 File Offset: 0x00110E68
		public static bool TryGet(MessageProperties properties, out ChannelBindingMessageProperty property)
		{
			if (properties == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("properties");
			}
			property = null;
			object obj;
			if (properties.TryGetValue(ChannelBindingMessageProperty.Name, out obj))
			{
				property = (obj as ChannelBindingMessageProperty);
				return property != null;
			}
			return false;
		}

		// Token: 0x06004AF8 RID: 19192 RVA: 0x00112CA9 File Offset: 0x00110EA9
		public void AddTo(Message message)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			this.AddTo(message.Properties);
		}

		// Token: 0x06004AF9 RID: 19193 RVA: 0x00112CCA File Offset: 0x00110ECA
		public void AddTo(MessageProperties properties)
		{
			this.ThrowIfDisposed();
			if (properties == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("properties");
			}
			properties.Add(ChannelBindingMessageProperty.Name, this);
		}

		// Token: 0x06004AFA RID: 19194 RVA: 0x00112CF4 File Offset: 0x00110EF4
		public IMessageProperty CreateCopy()
		{
			object obj = this.thisLock;
			lock (obj)
			{
				this.ThrowIfDisposed();
				this.refCount++;
			}
			return this;
		}

		// Token: 0x06004AFB RID: 19195 RVA: 0x00112D48 File Offset: 0x00110F48
		public void Dispose()
		{
			if (!this.IsDisposed)
			{
				object obj = this.thisLock;
				lock (obj)
				{
					if (!this.IsDisposed)
					{
						int num = this.refCount - 1;
						this.refCount = num;
						if (num == 0 && this.ownsCleanup)
						{
							((IDisposable)this.channelBinding).Dispose();
						}
					}
				}
			}
		}

		// Token: 0x06004AFC RID: 19196 RVA: 0x00112DB8 File Offset: 0x00110FB8
		private void ThrowIfDisposed()
		{
			if (this.IsDisposed)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ObjectDisposedException(base.GetType().FullName));
			}
		}

		// Token: 0x04002F28 RID: 12072
		private const string propertyName = "ChannelBindingMessageProperty";

		// Token: 0x04002F29 RID: 12073
		private ChannelBinding channelBinding;

		// Token: 0x04002F2A RID: 12074
		private object thisLock;

		// Token: 0x04002F2B RID: 12075
		private bool ownsCleanup;

		// Token: 0x04002F2C RID: 12076
		private int refCount;
	}
}
