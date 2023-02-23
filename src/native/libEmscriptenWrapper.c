#include <emscripten.h>

int run_script_int(const char* script)
{
	return emscripten_run_script_int(script);
}

char* run_script_string(const char* script)
{
	return emscripten_run_script_string(script);
}