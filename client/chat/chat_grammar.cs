IGO_RegisterChatHook("NMH_Grammar");

function NMH_Grammar(%msg)
{
	//--Short Hand Correction--------------------
	%tokens	 = "i,o,r,u,im,ne,ur,ima,pls,hes,shes,dont,theyre,playin,wut, ";
	%replace = "I,oh,are,you,I'm,any,you're,I'm going to,please,he's,she's,don't,they're,playing,what, ";
	%count	 = getWordCount(%msg);
	
	while((%tokens = nextToken(%tokens, "next", ",")) !$= "")
	{
		%replace = nextToken(%replace, "new", ",");
		for(%i=0; %i < %count; %i++)
			if(getWord(%msg, %i) $= %next)
				%msg = setWord(%msg, %i, %new);
	}
	
	//--Capitilize & Punctuation Discovery-------
	%tokens	= ".,?,!, ";
	%length	= strLen(%msg) - 1;
	%fstCha	= getSubStr(%msg, 0, 1);
	%endCha	= getSubStr(%msg, %length, 1);
	%msg	= strUpr(%fstCha) @ getSubStr(%msg, 1, %length);
	
	while((%tokens = nextToken(%tokens, "next", ",")) !$= "")
		if(%endCha $= %next)
			return %msg;	
	
	//--Punctuation Application----------------
	%tokens	= "where,what,when,why,how,";
	%tokens	= %count > 1 ? %tokens @ "will,did,do,does,is, " : %tokens @ " ";
	%first	= firstWord(%msg);
	
	while((%tokens = nextToken(%tokens, "next", ",")) !$= "")
		if(%first $= %next)
			return %msg @ "?";

	return %count > 1 ? %msg @ "." : %msg;
}