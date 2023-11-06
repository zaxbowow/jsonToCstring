# jsonToCstring
Converts a .json file into a c source code file as a named char array.  The json file is minimized before being saved as the named char array of your choice.  Supports UTF8 and works great with the mjson library.

Typical use is to include a .json file in your c project and to add a prebuild directive that converts the .json into a .c file that you can add to your project.  You can also use the -a parameter to append to existing c souce code file, in order have several json files appended to a single c source code file ans seperately named constants.

Command line use:

jsonToCstring.exe -n [const char variable name for output c file] -i [input file] -o [output file] [-a (optional append directive)]

jsonToCstring.exe -n menu_l0 -i "menu.language0.json" -o menu.c

Input file menu.language0.json:

{	"menu": 
	{ "d": "Instrument", "s": [ 
		{ "d": "Information", "s": [
			{ "d": "Serial number", "m": "SERIALNUM" },
			{ "d": "Manufacturer", "m": "MANUFACTURER" },
			{ "d": "Manufacture date", "m": "MANUFACTDATE" },
			{ "d": "EEPROM version", "m": "EEPROMVERSION" },
			{ "d": "Software ID", "m": "SOFTPKGID" },
			{ "d": "Strong signature verified", "m": "SIGVERIFIED" },			
			{ "d": "Pre-bootloader", "s": [
				{ "d": "ID", "m": "BOOT1SOFTID" },
				{ "d": "Version", "m": "BOOT1VERSION" },
				{ "d": "CRC", "m": "BOOT1CRC" },
				{ "d": "Compile date", "m": "BOOT1COMPDATE" },
				{ "d": "Compile time", "m": "BOOT1COMPTIME" } ] },
			{ "d": "Bootloader", "s": [
				{ "d": "ID", "m": "BOOT2SOFTID" },
				{ "d": "Version", "m": "BOOT2VERSION" },
				{ "d": "CRC", "m": "BOOT2CRC" },
				{ "d": "Compile date", "m": "BOOT2COMPDATE" },
				{ "d": "Compile time", "m": "BOOT2COMPTIME" },
				{ "d": "Counter", "m": "BOOT2COUNTER" } ] },
			{ "d": "Base code", "s": [
				{ "d": "ID", "m": "BASESOFTID" },
				{ "d": "Version", "m": "BASEVERSION" },
				{ "d": "CRC", "m": "BASECRC" },
				{ "d": "Compile date", "m": "BASECOMPDATE" },
				{ "d": "Compile time", "m": "BASECOMPTIME" } ] },
			{ "d": "Customer code", "s": [
				{ "d": "ID", "m": "CUSTSOFTID" },
				{ "d": "Version", "m": "CUSTVERSION" },
				{ "d": "CRC", "m": "CUSTCRC" },
				{ "d": "Compile date", "m": "CUSTCOMPDATE" },
				{ "d": "Compile time", "m": "CUSTCOMPTIME" },
				{ "d": "API version", "m": "CUSTAPIVER" } ] } ] },		
...

Output file menu.c:

const char menu_l0[] = "{\"menu\":{\"d\":\"Instrument\",\"s\":[{\"d\":\"Information\",\"s\":[{\"d\":\"Serial number\","
                       "\"m\":\"SERIALNUM\"},{\"d\":\"Manufacturer\",\"m\":\"MANUFACTURER\"},{\"d\":\"Manufacture date"
                       "\",\"m\":\"MANUFACTDATE\"},{\"d\":\"EEPROM version\",\"m\":\"EEPROMVERSION\"},{\"d\":\"Software"
                       " ID\",\"m\":\"SOFTPKGID\"},{\"d\":\"Strong signature verified\",\"m\":\"SIGVERIFIED\"},{\"d\":"
                       "\"Pre-bootloader\",\"s\":[{\"d\":\"ID\",\"m\":\"BOOT1SOFTID\"},{\"d\":\"Version\",\"m\":\"BOOT1"
                       "VERSION\"},{\"d\":\"CRC\",\"m\":\"BOOT1CRC\"},{\"d\":\"Compile date\",\"m\":\"BOOT1COMPDATE\"},"
                       "{\"d\":\"Compile time\",\"m\":\"BOOT1COMPTIME\"}]},{\"d\":\"Bootloader\",\"s\":[{\"d\":\"ID\","
                       "\"m\":\"BOOT2SOFTID\"},{\"d\":\"Version\",\"m\":\"BOOT2VERSION\"},{\"d\":\"CRC\",\"m\":\"BOOT2C"
                       "RC\"},{\"d\":\"Compile date\",\"m\":\"BOOT2COMPDATE\"},{\"d\":\"Compile time\",\"m\":\"BOOT2COM"
                       "PTIME\"},{\"d\":\"Counter\",\"m\":\"BOOT2COUNTER\"}]},{\"d\":\"Base code\",\"s\":[{\"d\":\"ID\""
                       ",\"m\":\"BASESOFTID\"},{\"d\":\"Version\",\"m\":\"BASEVERSION\"},{\"d\":\"CRC\",\"m\":\"BASECRC"
                       "\"},{\"d\":\"Compile date\",\"m\":\"BASECOMPDATE\"},{\"d\":\"Compile time\",\"m\":\"BASECOMPTIM"
                       "E\"}]},{\"d\":\"Customer code\",\"s\":[{\"d\":\"ID\",\"m\":\"CUSTSOFTID\"},{\"d\":\"Version\","
                       "\"m\":\"CUSTVERSION\"},{\"d\":\"CRC\",\"m\":\"CUSTCRC\"},{\"d\":\"Compile date\",\"m\":\"CUSTCO"
                       "MPDATE\"},{\"d\":\"Compile time\",\"m\":\"CUSTCOMPTIME\"},{\"d\":\"API version\",\"m\":\"CUSTAP"
                       "IVER\"}]}]},{\"d\":\"Status\",\"s\":[{\"d\":\"Access level\",\"m\":\"ACCESSLEVEL\"},{\"d\":\"Te"
                       "st series\",\"m\":\"TESTSEQRESET\"},{\"d\":\"Test number\",\"m\":\"TESTSEQCOUNT\"},{\"d\":\"Dat"
                       "e\",\"m\":\"CURRDATE\"},{\"d\":\"Time\",\"m\":\"CURRTIME\"},{\"d\":\"Last clock adjustment\",\""
                       "m\":\"LASTTIMESET\"},{\"d\":\"Power\",\"s\":[{\"d\":\"Real time clock voltage\",\"m\":\"RTCBATT"

...
