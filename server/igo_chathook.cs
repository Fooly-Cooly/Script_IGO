function IGO_RegisterServerChatHook(%hook, %state)
{
	if(!isObject(IGO_SvrChatHook))
		new ScriptObject(IGO_SvrChatHook){ count = 0; };
	
	%obj = IGO_SvrChatHook;
	if(!%obj.hook[%hook])
	{
		eval(%obj @ "." @%hook @ "=" @ %state @ ";");
		%obj.count++;
	}
}

package IGO_ChatHook
{
	function serverCmdMessageSent(%cl, %msg)
	{
			for(%i=0; %i <	%cnt+1; %i++)
		if( $= %hook)
			return;
	
	
		while((%hook = getField(%obj.getTaggedField(%i), 1)) !$= "")
		{
			
		}
	

		getField(%obj.getTaggedField(%i), 1)
		for(%i=0; %i < IGO_SvrChatHook.count; %i++)
			%msg = call(IGO_SvrChatHook.cmd[%i] ,%cl, %msg);
		
		if(%msg !$= "")
			parent::serverCmdMessageSent(%cl, %msg);
	}
};
ActivatePackage(IGO_ChatHook);

exec("./chat/chat_flood.cs");