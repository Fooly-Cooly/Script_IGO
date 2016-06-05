function IGO_AdminChat(%val)
{
	if(!%val)
	{
		newMessageHud.channel = "Admin";
		NMH_Channel.setValue("\c2Admin:");
		canvas.pushdialog("newMessageHud");
	}
}

function IGO_RegisterChatHook(%hook)
{
	if(!$ChatHook::Num)
		$ChatHook::Num = 0;
	if(!$ChatHook::Send[%hook])
	{
		$ChatHook::Send[%hook] = 2;
		$ChatHook::Send[$ChatHook::Num] = %hook;
		$ChatHook::Num++;
	}
}

package IGO_ChatHook
{
	function newMessageHud::onWake(%this)
	{
		%this.updatePosition();
		%this.updateTypePosition();
		Parent::onWake(%this);
	}

	function NMH_Type::send(%this)
	{
		if((%msg = %this.getValue()) !$= "")
			switch$(getSubStr(%msg, 0, 1))
			{
				case "!":
					%arg = strReplace(restWords(%msg), " ", ","); 
					%cmd = getSubStr(%msg, 1, strlen(getWord(%msg, 0)));
					eval(%cmd@"("@%arg@");");
				default:
					for(%i=0; %i < $ChatHook::Num; %i++)
					{
						%hook = $ChatHook::Send[%i];
						if($ChatHook::Send[%hook] == 2)
							%msg = (%callback = call(%hook, %msg)) !$= "" ? %callback : %msg;
					}
			}
		%this.setValue(%msg);
		Parent::Send(%this);
	}
};
ActivatePackage(IGO_ChatHook);