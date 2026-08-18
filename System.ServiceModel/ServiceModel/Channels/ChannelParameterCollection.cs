using System;
using System.Collections.ObjectModel;

namespace System.ServiceModel.Channels
{
	// Token: 0x020006FF RID: 1791
	[__DynamicallyInvokable]
	public class ChannelParameterCollection : Collection<object>
	{
		// Token: 0x0600448E RID: 17550 RVA: 0x0010260A File Offset: 0x0010080A
		[__DynamicallyInvokable]
		public ChannelParameterCollection()
		{
		}

		// Token: 0x0600448F RID: 17551 RVA: 0x00102612 File Offset: 0x00100812
		[__DynamicallyInvokable]
		public ChannelParameterCollection(IChannel channel)
		{
			this.channel = channel;
		}

		// Token: 0x170011B5 RID: 4533
		// (get) Token: 0x06004490 RID: 17552 RVA: 0x00102621 File Offset: 0x00100821
		[__DynamicallyInvokable]
		protected virtual IChannel Channel
		{
			[__DynamicallyInvokable]
			get
			{
				return this.channel;
			}
		}

		// Token: 0x06004491 RID: 17553 RVA: 0x0010262C File Offset: 0x0010082C
		[__DynamicallyInvokable]
		public void PropagateChannelParameters(IChannel innerChannel)
		{
			if (innerChannel == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("innerChannel");
			}
			this.ThrowIfMutable();
			ChannelParameterCollection property = innerChannel.GetProperty<ChannelParameterCollection>();
			if (property != null)
			{
				for (int i = 0; i < base.Count; i++)
				{
					property.Add(base[i]);
				}
			}
		}

		// Token: 0x06004492 RID: 17554 RVA: 0x0010267A File Offset: 0x0010087A
		[__DynamicallyInvokable]
		protected override void ClearItems()
		{
			this.ThrowIfDisposedOrImmutable();
			base.ClearItems();
		}

		// Token: 0x06004493 RID: 17555 RVA: 0x00102688 File Offset: 0x00100888
		[__DynamicallyInvokable]
		protected override void InsertItem(int index, object item)
		{
			this.ThrowIfDisposedOrImmutable();
			base.InsertItem(index, item);
		}

		// Token: 0x06004494 RID: 17556 RVA: 0x00102698 File Offset: 0x00100898
		[__DynamicallyInvokable]
		protected override void RemoveItem(int index)
		{
			this.ThrowIfDisposedOrImmutable();
			base.RemoveItem(index);
		}

		// Token: 0x06004495 RID: 17557 RVA: 0x001026A7 File Offset: 0x001008A7
		[__DynamicallyInvokable]
		protected override void SetItem(int index, object item)
		{
			this.ThrowIfDisposedOrImmutable();
			base.SetItem(index, item);
		}

		// Token: 0x06004496 RID: 17558 RVA: 0x001026B8 File Offset: 0x001008B8
		private void ThrowIfDisposedOrImmutable()
		{
			IChannel channel = this.Channel;
			if (channel != null)
			{
				CommunicationState state = channel.State;
				string text = null;
				if (state != CommunicationState.Created)
				{
					if (state - CommunicationState.Opening <= 4)
					{
						text = SR.GetString("ChannelParametersCannotBeModified", new object[]
						{
							channel.GetType().ToString(),
							state.ToString()
						});
					}
					else
					{
						text = SR.GetString("CommunicationObjectInInvalidState", new object[]
						{
							channel.GetType().ToString(),
							state.ToString()
						});
					}
				}
				if (text != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(text));
				}
			}
		}

		// Token: 0x06004497 RID: 17559 RVA: 0x00102758 File Offset: 0x00100958
		private void ThrowIfMutable()
		{
			IChannel channel = this.Channel;
			if (channel != null)
			{
				CommunicationState state = channel.State;
				string text = null;
				if (state != CommunicationState.Created)
				{
					if (state - CommunicationState.Opening > 4)
					{
						text = SR.GetString("CommunicationObjectInInvalidState", new object[]
						{
							channel.GetType().ToString(),
							state.ToString()
						});
					}
				}
				else
				{
					text = SR.GetString("ChannelParametersCannotBePropagated", new object[]
					{
						channel.GetType().ToString(),
						state.ToString()
					});
				}
				if (text != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(text));
				}
			}
		}

		// Token: 0x04002D3B RID: 11579
		private IChannel channel;
	}
}
