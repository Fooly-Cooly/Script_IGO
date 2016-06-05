if(!isObject(IGO_ChatHook_Server))
{
	new ScriptObject(IGO_ChatHook_Svr)
	{
		cmd0 = "IGO_Commands";
		cmd1 = "IGO_FloodProtection";
		count = 2;
	};
}

//Needs more work but is coming along nicely
function IGO_FloodProtection(%cl,%msg)
{
	%msg = trim(%msg);
	%cnt = strlen(%msg);
	%dif = strlen(%cl.lastMsg);
	%dif = MAbs(%cnt - %dif);
	for(%i=0; %i < %cnt; %i++)
	{
		%char0 = getSubStr(%msg, %i, 1);
		%char1 = getSubStr(%cl.lastMsg, %i+%dif, 1);
		if(%char0 $= %char1) %chk++;
	}
	%chk = (%chk * 100) / %cnt;
	%cl.lastMsg = %msg;
	if(%chk > 60 && %cl.lastMsgTime > $Sim::Time)
		return;
	%cl.lastMsgTime = $Sim::Time + 3;
	return %msg;
}

function IGO_Commands(%cl,%msg)
{
	if(getSubStr(%msg, 0, 1) !$= "!")
		return %msg;
	%cmd = firstWord(%msg);
	%msg = restWords(%msg);
	%cmd = "IGOCmd_" @ stripChars(%cmd, "!");
	if(isFunction(%cmd))
		call(%cmd, %cl, %msg);
}

package IGO_ChatHook
{
	function serverCmdMessageSent(%cl, %msg)
	{
		for(%i=0; %i<IGO_ChatHook_Svr.count; %i++)
			%msg = call(IGO_ChatHook_Svr.cmd[%i] ,%cl, %msg);
		if(%msg !$= "")
			parent::serverCmdMessageSent(%cl, %msg);
	}
};
ActivatePackage(IGO_ChatHook);