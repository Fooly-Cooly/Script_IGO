function IGO_Commands(%cl,%msg)
{
	if(getSubStr(%msg, 0, 1) !$= "!")
		return %msg;
	%cmd = firstWord(%msg);
	%msg = restWords(%msg);
	%cmd = "IGOCmd_" @ stripChars(%cmd, "!");
	if(isFunction(%cmd))
		call(%cmd, %cl, %msg);
	return;
}