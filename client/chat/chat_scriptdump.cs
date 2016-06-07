IGO_RegisterClientChatHook("NMH_ScriptDump");

function NMH_ScriptDump(%msg)
{
	//--Check for Command------------------------
	if(firstWord(%msg) $= "@dump")
	{
		//--Open File for Read-----------------------
		%fileN = getWord(%msg, 1);
		%file = new FileObject();
		%error = %file.openForRead(%fileN);
		
		//--Invalid File Clean-up--------------------
		if(%error)
		{
			%file.close();
			%file.delete();
			return "404 - File Not Found"
		}

		//--Read Lines, Clean-up & Set Callback------
		while(!%file.isEOF())
			%line = %line SPC %file.readLine();
		%file.close();
		%file.delete();
		%msg = "@" @ %line;
	}
	return %msg;
}