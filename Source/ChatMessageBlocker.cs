using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Memory;
using CounterStrikeSharp.API.Modules.Memory.DynamicFunctions;

namespace ChatMessageBlocker;

public class ChatMessageBlocker : BasePlugin
{
	public override string ModuleName => "Chat Message Blocker";
	public override string ModuleVersion => "1.0.0";
	public override string ModuleAuthor => "LadderGoat";
	public override string ModuleDescription => "Filters and blocks chat messages.";

	readonly string[] chatMessages = ChatMessages.messagesArray;

	public override void Load(bool hotReload)
	{
		VirtualFunctions.ClientPrintFunc.Hook(OnPrintToChat, HookMode.Pre);
		VirtualFunctions.ClientPrintAllFunc.Hook(OnPrintToChatAll, HookMode.Pre);
		
		base.Load(hotReload);
	}

	public override void Unload(bool hotReload)
	{
		base.Unload(hotReload);
	}
	
	private HookResult OnPrintToChat(DynamicHook hook)
	{
		return InternalHandler(hook.GetParam<string>(2));
	}
	
	private HookResult OnPrintToChatAll(DynamicHook hook)
	{
		return InternalHandler(hook.GetParam<string>(1));
	}
	
	private HookResult InternalHandler(string message)
	{
		for(int i = 0; i < chatMessages.Length; i++)
		{
			if(message.Contains(chatMessages[i]))
			{
				return HookResult.Handled;
			}
		}
		return HookResult.Continue;
	}
}

