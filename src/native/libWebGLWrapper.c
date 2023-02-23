#include <emscripten\html5_webgl.h>
#include <GLES2\gl2.h>

EMSCRIPTEN_WEBGL_CONTEXT_HANDLE webgl_create_context(const char* target, const EmscriptenWebGLContextAttributes* attributes) {
	return emscripten_webgl_create_context(target, attributes);
}

void webgl_init_context_attributes(EmscriptenWebGLContextAttributes* attributes) {
	emscripten_webgl_init_context_attributes(attributes);
}

EMSCRIPTEN_RESULT webgl_make_context_current(EMSCRIPTEN_WEBGL_CONTEXT_HANDLE context) {
	return emscripten_webgl_make_context_current(context);
}

GL_APICALL void GL_APIENTRY webgl_clearColor(GLfloat red, GLfloat green, GLfloat blue, GLfloat alpha) {
	glClearColor(red, green, blue, alpha);
}

GL_APICALL void GL_APIENTRY webgl_clear(GLbitfield mask) {
	glClear(mask);
}