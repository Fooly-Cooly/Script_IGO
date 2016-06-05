IGO_RegisterChatHook("NMH_ClientExec");

function NMH_ClientExec(%msg)
{
	//--Check for Command------------------------
	if(firstWord(%msg) $= "@exec")
	{
		//--Get File, Execute & Callback-------------
		%file	= getWord(%msg, 1);
		%error	= exec(%file);
		%msg	= %error ? "@Executed \"" @ %file @ "\"" : "404 - File Not Found"
	}
	return %msg;
}