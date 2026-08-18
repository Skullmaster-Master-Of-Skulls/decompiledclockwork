using System;
using System.Reflection;
using System.Threading;

// Token: 0x020004D4 RID: 1236
internal class sprᴦ
{
	// Token: 0x06004BE5 RID: 19429 RVA: 0x002E8038 File Offset: 0x002E7038
	public void ᜀ(EventHandler A_0)
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (false)
			{
			}
			for (;;)
			{
				EventHandler eventHandler = this.ᜂ;
				int num = 1;
				for (;;)
				{
					if (true)
					{
					}
					EventHandler eventHandler2;
					switch (num)
					{
					case 0:
						goto IL_84;
					case 1:
						goto IL_49;
					case 2:
						if (eventHandler == eventHandler2)
						{
							num = 0;
							continue;
						}
						goto IL_49;
					}
					break;
					IL_49:
					eventHandler2 = eventHandler;
					EventHandler value = (EventHandler)Delegate.Combine(eventHandler2, A_0);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.ᜂ, value, eventHandler2);
					num = 2;
				}
			}
			IL_84:
			break;
		}
	}

	// Token: 0x06004BE6 RID: 19430 RVA: 0x002E80CC File Offset: 0x002E70CC
	public void ᜁ(EventHandler A_0)
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (false)
			{
			}
			for (;;)
			{
				EventHandler eventHandler = this.ᜂ;
				int num = 0;
				for (;;)
				{
					if (true)
					{
					}
					EventHandler eventHandler2;
					switch (num)
					{
					case 0:
						goto IL_49;
					case 1:
						goto IL_84;
					case 2:
						if (eventHandler == eventHandler2)
						{
							num = 1;
							continue;
						}
						goto IL_49;
					}
					break;
					IL_49:
					eventHandler2 = eventHandler;
					EventHandler value = (EventHandler)Delegate.Remove(eventHandler2, A_0);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.ᜂ, value, eventHandler2);
					num = 2;
				}
			}
			IL_84:
			break;
		}
	}

	// Token: 0x06004BE7 RID: 19431 RVA: 0x002E8160 File Offset: 0x002E7160
	public void ᜃ(EventHandler A_0)
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (false)
			{
			}
			for (;;)
			{
				EventHandler eventHandler = this.ᜃ;
				int num = 1;
				for (;;)
				{
					EventHandler eventHandler2;
					switch (num)
					{
					case 0:
						if (eventHandler == eventHandler2)
						{
							if (true)
							{
							}
							num = 2;
							continue;
						}
						goto IL_41;
					case 1:
						goto IL_41;
					case 2:
						goto IL_84;
					}
					break;
					IL_41:
					eventHandler2 = eventHandler;
					EventHandler value = (EventHandler)Delegate.Combine(eventHandler2, A_0);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.ᜃ, value, eventHandler2);
					num = 0;
				}
			}
			IL_84:
			break;
		}
	}

	// Token: 0x06004BE8 RID: 19432 RVA: 0x002E81F4 File Offset: 0x002E71F4
	public void ᜂ(EventHandler A_0)
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (false)
			{
			}
			for (;;)
			{
				EventHandler eventHandler = this.ᜃ;
				int num = 1;
				for (;;)
				{
					EventHandler eventHandler2;
					switch (num)
					{
					case 0:
						goto IL_84;
					case 1:
						goto IL_41;
					case 2:
						if (eventHandler == eventHandler2)
						{
							if (true)
							{
							}
							num = 0;
							continue;
						}
						goto IL_41;
					}
					break;
					IL_41:
					eventHandler2 = eventHandler;
					EventHandler value = (EventHandler)Delegate.Remove(eventHandler2, A_0);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.ᜃ, value, eventHandler2);
					num = 2;
				}
			}
			IL_84:
			break;
		}
	}

	// Token: 0x06004BE9 RID: 19433 RVA: 0x002E8288 File Offset: 0x002E7288
	public spr\u2429[] ᜃ()
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
		return this.ᜀ;
	}

	// Token: 0x06004BEA RID: 19434 RVA: 0x002E82CC File Offset: 0x002E72CC
	public void ᜀ(spr\u2429[] A_0)
	{
		for (;;)
		{
			IL_00:
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 2:
					this.ᜀ = A_0;
					this.ᜁ();
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				}
				if (true)
				{
				}
				if (A_0 == this.ᜀ)
				{
					return;
				}
				num = 2;
			}
		}
	}

	// Token: 0x06004BEB RID: 19435 RVA: 0x002E8350 File Offset: 0x002E7350
	public FieldInfo[] ᜂ()
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
		return this.ᜁ;
	}

	// Token: 0x06004BEC RID: 19436 RVA: 0x002E8394 File Offset: 0x002E7394
	public void ᜀ(FieldInfo[] A_0)
	{
		for (;;)
		{
			IL_00:
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					return;
				case 2:
					this.ᜁ = A_0;
					this.ᜀ();
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				}
				if (A_0 == this.ᜁ)
				{
					return;
				}
				num = 2;
			}
		}
	}

	// Token: 0x06004BED RID: 19437 RVA: 0x002E8418 File Offset: 0x002E7418
	private void ᜁ()
	{
		for (;;)
		{
			IL_00:
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜂ(this, EventArgs.Empty);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 2:
					return;
				}
				if (true)
				{
				}
				if (this.ᜂ == null)
				{
					return;
				}
				num = 0;
			}
		}
	}

	// Token: 0x06004BEE RID: 19438 RVA: 0x002E849C File Offset: 0x002E749C
	private void ᜀ()
	{
		for (;;)
		{
			IL_00:
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					return;
				case 2:
					this.ᜃ(this, EventArgs.Empty);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						num = 1;
						continue;
					}
					break;
				}
				if (this.ᜃ == null)
				{
					return;
				}
				num = 2;
			}
		}
	}

	// Token: 0x06004BEF RID: 19439 RVA: 0x002E8520 File Offset: 0x002E7520
	private sprᴦ()
	{
	}

	// Token: 0x06004BF0 RID: 19440 RVA: 0x002E8534 File Offset: 0x002E7534
	public sprᴦ(spr\u2429[] A_0, FieldInfo[] A_1)
	{
		this.ᜀ = A_0;
		this.ᜁ = A_1;
	}

	// Token: 0x04002286 RID: 8838
	private spr\u2429[] ᜀ;

	// Token: 0x04002287 RID: 8839
	private FieldInfo[] ᜁ;

	// Token: 0x04002288 RID: 8840
	private EventHandler ᜂ;

	// Token: 0x04002289 RID: 8841
	private EventHandler ᜃ;
}
