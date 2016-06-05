IGO_RegisterChatHook("NMH_LinkChat");

function NMH_LinkChat(%msg)
{
	//--Replace Illegal Characters---------------
	%tokens	= "http://,:,<,>, ";

	while((%tokens = nextToken(%tokens, "next", ",")) !$= "")
		%msg = strReplace(%msg, %next, "");

	//--Append URL Tag---------------------------
	%msg = strReplace(%msg, " ", "_");
	return "http://" @ %msg;
}