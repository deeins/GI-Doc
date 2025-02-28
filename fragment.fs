//Most code from https://www.shadertoy.com/view/3d2XRd
//Add some comments,change the data structure and remove shading since all i need is depth

#define bunny
//#define shortarray
//#define notdiy
const float root_size = 3.;
const int levels = 5;
const int MAX_ITER = 222 ;

const float Power = 4.;
const float Bailout = 1.5;
const int Iterations = 6;
vec3 testColor = vec3(0.0, 0.0, 0.0);

#ifdef bunny
#ifdef shortarray
#ifdef notdiy
int voxels[64] = int[64](1,1,1,1,3,1,0,3, 
                        0,2,2,2,0,2,0,0,
                        0,0,0,4,4,0,4,0,
						0,0,4,0,0,4,0,4,
                         5,5,5,0,5,0,0,0,
                         0,0,0,0,6,0,0,0,
                         0,7,0,0,7,0,7,0,
                       	-1,-1,-1,-1,-1,-1,-1,0);
#else
int voxels[56] = int[56](1,1,1,1,3,1,0,3, 
                        0,2,2,2,0,2,0,0,
                        0,0,0,4,4,0,4,0,
						0,0,4,0,0,4,0,4,
                        5,5,5,0,5,0,0,0,
                         0,0,0,0,6,0,0,0,
                         0,7,0,0,7,0,7,0);
//int voxels[64];
#endif
#else
int voxels[1840] = int[1840](1,1,1,1,1,1,1,1,8,11,2,44,116,122,154,161,0,0,3,0,63,57,36,47,0,5,0,4,0,6,0,7,0,0,0,0,-1,-1,-1,-1,0,0,0,0,0,0,0,-1,-1,0,-1,-1,-1,0,-1,-1,-1,-1,-1,0,-1,-1,0,0,0,18,0,9,68,21,53,28,0,0,0,0,0,10,0,14,0,0,0,0,0,-1,-1,-1,16,0,12,0,24,50,30,41,0,0,0,0,13,20,15,0,0,0,0,0,-1,-1,-1,-1,0,0,0,0,-1,-1,0,0,0,0,0,0,-1,-1,0,0,0,0,0,0,0,0,17,0,0,0,0,0,0,0,-1,-1,0,0,0,0,0,0,0,19,0,0,0,0,0,0,0,-1,0,0,0,0,-1,0,-1,0,22,23,49,26,70,72,74,75,0,0,0,-1,0,0,-1,-1,0,0,-1,-1,0,0,-1,-1,39,40,25,27,73,71,76,77,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,52,29,43,33,80,81,87,88,-1,-1,-1,-1,-1,-1,-1,-1,31,32,34,35,82,83,89,90,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,0,37,0,38,115,99,0,0,-1,-1,-1,-1,-1,-1,-1,-1,-1,0,0,0,0,0,0,0,0,0,-1,-1,0,0,-1,-1,0,0,-1,0,0,0,-1,-1,42,0,56,0,84,113,91,0,-1,0,-1,0,-1,-1,-1,-1,0,-1,0,0,-1,-1,-1,-1,0,0,0,0,60,65,45,0,0,0,46,0,0,0,0,0,-1,0,-1,0,-1,0,0,0,0,67,0,48,100,101,102,103,0,0,0,-1,-1,-1,-1,-1,0,0,0,0,0,-1,0,0,0,0,51,0,104,0,78,106,0,0,0,0,-1,0,-1,0,0,0,0,0,-1,-1,-1,-1,0,54,0,55,107,79,85,86,0,0,0,0,0,-1,-1,-1,0,0,0,0,-1,-1,-1,-1,0,0,0,0,-1,-1,-1,0,58,59,0,0,93,94,110,114,0,0,0,0,-1,-1,-1,-1,0,0,0,0,-1,-1,-1,-1,61,62,0,0,95,96,111,112,0,0,0,0,-1,-1,-1,-1,0,0,0,0,-1,-1,-1,-1,0,64,0,0,108,92,109,98,0,0,0,0,-1,-1,0,0,66,0,0,0,97,0,0,0,0,0,0,0,-1,0,0,0,0,0,0,0,0,0,-1,-1,0,0,0,0,0,69,0,105,0,0,-1,-1,0,0,-1,-1,0,0,-1,-1,0,0,-1,-1,0,0,-1,-1,0,0,-1,-1,0,0,-1,-1,0,0,-1,-1,0,0,-1,-1,0,0,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,0,-1,0,-1,0,-1,0,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,0,-1,-1,-1,0,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,0,-1,-1,0,-1,-1,-1,-1,-1,-1,-1,-1,-1,0,0,-1,-1,0,0,-1,0,0,-1,0,0,0,-1,0,0,-1,0,-1,0,0,0,0,0,0,0,-1,0,0,0,-1,-1,0,-1,0,0,0,-1,0,-1,-1,0,-1,0,-1,0,-1,-1,0,0,0,-1,0,0,0,-1,0,-1,0,-1,0,-1,-1,-1,0,-1,0,-1,-1,-1,0,-1,-1,-1,0,0,-1,-1,-1,0,-1,-1,0,0,-1,-1,0,0,-1,0,0,0,-1,-1,0,0,0,0,0,0,-1,-1,0,0,0,0,0,0,-1,-1,0,0,0,0,0,0,0,-1,0,0,117,119,135,137,0,215,0,223,0,118,0,134,0,178,0,183,0,0,0,-1,0,0,0,-1,120,121,128,129,179,181,184,185,0,0,-1,-1,0,0,-1,-1,0,0,-1,-1,0,0,-1,-1,123,126,140,143,217,0,220,0,124,125,130,131,182,180,186,187,0,0,-1,-1,0,0,-1,-1,0,0,-1,-1,0,0,-1,-1,127,0,132,133,0,0,188,0,0,0,-1,-1,0,0,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,0,-1,-1,-1,0,-1,-1,0,0,0,-1,0,0,0,-1,146,136,147,148,195,189,196,197,-1,-1,-1,-1,-1,-1,-1,-1,138,139,149,150,190,191,198,199,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,141,142,151,152,192,193,200,201,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,144,145,153,0,194,0,202,0,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,0,0,-1,-1,0,0,0,-1,0,-1,0,0,0,-1,-1,-1,0,-1,0,-1,0,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,155,158,173,176,228,0,0,0,156,157,167,168,209,203,210,211,0,-1,-1,-1,0,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,159,160,169,170,204,205,212,0,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,162,165,0,0,0,0,0,0,163,164,171,172,206,207,0,0,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,166,0,0,0,208,0,0,0,-1,-1,-1,0,-1,-1,-1,0,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,0,-1,0,-1,0,-1,-1,0,0,0,0,0,0,-1,-1,0,0,-1,-1,0,0,-1,-1,0,0,-1,-1,0,0,174,175,0,0,213,214,0,0,-1,-1,0,0,-1,-1,0,0,-1,-1,0,0,-1,-1,0,0,177,0,0,0,0,0,0,0,-1,0,0,0,-1,0,0,0,0,0,0,-1,0,0,0,-1,0,0,-1,-1,0,0,-1,-1,0,0,-1,-1,0,0,-1,-1,0,0,-1,-1,0,0,-1,-1,0,0,-1,-1,0,0,-1,-1,0,-1,0,0,0,0,0,0,-1,-1,-1,-1,-1,0,0,0,-1,-1,-1,-1,-1,-1,0,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,0,-1,-1,0,0,-1,0,-1,-1,-1,-1,0,0,0,-1,-1,-1,-1,-1,0,0,-1,-1,-1,-1,-1,-1,0,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,0,-1,0,0,0,0,-1,0,0,0,0,0,-1,0,-1,0,0,0,0,-1,-1,-1,-1,0,-1,0,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,0,-1,0,-1,-1,-1,-1,-1,0,-1,-1,-1,-1,-1,-1,0,0,0,0,-1,-1,-1,-1,0,0,0,0,-1,-1,-1,-1,-1,-1,0,0,-1,-1,-1,-1,-1,0,0,0,-1,0,0,0,0,0,0,0,0,-1,-1,-1,0,-1,-1,-1,-1,-1,-1,-1,-1,-1,0,-1,-1,-1,-1,-1,-1,-1,-1,0,-1,0,0,0,0,0,0,0,0,-1,0,0,0,0,0,0,-1,-1,0,0,0,0,0,0,0,216,0,0,0,0,0,0,0,0,-1,-1,0,0,0,0,0,0,218,219,0,0,0,0,0,0,-1,-1,0,0,0,0,0,0,-1,0,0,0,0,0,221,222,226,227,0,0,0,0,-1,-1,-1,-1,0,0,0,0,-1,0,-1,-1,0,0,0,0,0,224,0,225,0,0,0,0,0,-1,0,-1,0,0,0,0,0,-1,0,0,0,0,0,0,-1,-1,-1,-1,0,0,0,0,-1,0,0,0,0,0,0,0,0,229,0,0,0,0,0,0,0,0,-1,0,0,0,0,0);
//int voxels[1840];
#endif
#endif
const float[6] Scales = float[6](1.,.5,.25,.125,.0625,.03125);

// -----------------------------------------------------------------------------------------

float dist(vec3 pos) {
    // This function takes ~all of the rendering time, the trigonometry is super expensive
    // So if there are any faster approximations, they should definitely be used
	vec3 z = pos;
	float dr = 1.0;
	float r = 0.0;
    for (int i = 0; i < Iterations; i++) {
		r = length(z);
		if (r>Bailout) break;
        
		// convert to polar coordinates
		float theta = acos(z.z/r);

		float phi = atan(z.y,z.x);

		dr = pow( r, Power-1.0)*Power*dr + 1.0;
		
		// scale and rotate the point
		float zr = pow( r,Power);
		theta = theta*Power;
		phi = phi*Power;
		
		// convert back to cartesian coordinates
		z = zr*vec3(sin(theta)*cos(phi), sin(phi)*sin(theta), cos(theta));
        z+=pos;
	}
	return 0.5*log(r)*r/dr;
}


vec2 isect(in vec3 pos, in float size, in vec3 ro, in vec3 rd, out vec3 tmid, out vec3 tmax) {
    vec3 mn = pos - 0.5 * size;
    vec3 mx = mn + size;
    vec3 t1 = (mn-ro) / rd;
    vec3 t2 = (mx-ro) / rd;
    vec3 tmin = min(t1, t2);
    tmax = max(t1, t2);
    tmid = (pos-ro)/rd; // tmax;
    return vec2(max(tmin.x, max(tmin.y, tmin.z)), min(tmax.x, min(tmax.y, tmax.z)));
}


const float d_corner = sqrt(0.75);

bool trace(in vec3 ro, in vec3 rd, out vec2 t, out vec3 pos, out int iter, out float size) {
    
    struct ST {
    vec3 pos;
	int scale; // size = root_size * exp2(float(-scale));
    vec3 idx;
    int ptr;
    float h;
	} stack[levels];

	int stack_ptr = 0; // Next open index


    
    //-- INITIALIZE --//
    
    
    size = root_size;//3.
    vec3 root_pos = vec3(0.,0.,0.);
    pos = root_pos;
    vec3 tmid;
    vec3 tmax;
    bool can_push = true;
    float d;
    t = isect(pos, size, ro, rd, tmid, tmax);
    float h = t.y;
    
    // Initial push, sort of
    // If the minimum is before the middle in this axis, we need to go to the first one (-rd)
    vec3 idx = mix(-sign(rd), sign(rd), lessThanEqual(tmid, vec3(t.x)));
    
    if(idx == vec3(-1.0,-1.,1.))testColor = vec3(0.0, 0.8, 0.6);
    if(idx == vec3(-1.,1.,-1.0))testColor = vec3(0.0, 0.2, 0.4);
    if(idx == vec3(1.0,-1.,-1.))testColor = vec3(0.0, 0.7, 0.1);
    if(idx == vec3(1.0,1.,-1.0))testColor = vec3(0.0, 0.0, 0.5);
    if(idx == vec3(1.,-1.0,1.))testColor = vec3(0.0, 0.5, 0.0);
    if(idx == vec3(-1.,1.0,1.))testColor = vec3(0.0, 0.1, 0.6);
    if(idx == vec3(1.0))testColor = vec3(0.0, 1.0, 0.0);
    if(idx == vec3(-1.0))testColor = vec3(0.0, 0.0, 1.0);
    int stackIdx = 0;
   	int scale = 1;
    size *= 0.5;//level 1 size
    pos += 0.5 * size * idx;//move to first hitted sub-cell center
    
    
    iter = MAX_ITER;
    while (iter --> 0) {
        t = isect(pos, size, ro, rd, tmid, tmax);
        
        #ifndef bunny
        d = dist(pos);
        #endif
        
        #ifdef bunny

        float subIdx = dot(idx*.5+.5,vec3(1.,2.,4.));
        int curIdx = stackIdx*8+int(subIdx);
        
        if (voxels[curIdx] != 0) { // Voxel exists
        #else
            if(d <size*d_corner){
        #endif    
            if (scale >= levels)// //hit the smallest voxel;
                return true;
            
            if (can_push) {
                //-- PUSH --//
                
                
                if (t.y < h) //*t.y is this voxel exist dist,h is parent voxel exist dist
                {
                    
                    stack[stack_ptr++] = ST(pos, scale, idx,stackIdx, h);
                }
                h = t.y;
                scale++;
                size *= 0.5;
                idx = mix(-sign(rd), sign(rd), step(tmid, vec3(t.x)));
                
                #ifdef bunny
                stackIdx = voxels[curIdx];
                #else
                stackIdx = 0;
                #endif
                
                pos += 0.5 * size * idx;
                continue;
            }
        }
        
        //when code still running,means (!voxel Exist  || can_push == false) 
        //-- ADVANCE --//
        
        // Advance for every direction where we're hitting the middle (tmax = tmid)
        vec3 old = idx;
        
        //this is genius,for the hitted direction,if hit point is in the middle,we advance to the other side,
		//else if hit point is in the edge,it will leave the stack.and keep all unhitted direction
        idx = mix(idx, sign(rd), equal(tmax, vec3(t.y)));
        
        //if old = idx → stay,else → move forward in this stack
        pos += mix(vec3(0.), sign(rd), notEqual(old, idx)) * size;
        
        // If idx hasn't changed, we're at the last child in this voxel
        if (idx == old) {
            //-- POP --//
           
            if (stack_ptr == 0 || scale == 0) return false; //exist Whole Octree;
            
            ST s = stack[--stack_ptr];//Back to parent Stack
            pos = s.pos;
            scale = s.scale;
            //size = root_size * exp2(float(-scale));
            size = root_size * Scales[scale];
			idx = s.idx;
			stackIdx = s.ptr;
            h = s.h;
            
            
            can_push = false; //*once stack pop out,get rid out pushing in again
        } else can_push = true;//idx != old  move forward in this stack
    }
    
    return false;
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    vec2 uv = fragCoord / iResolution.xy;
    //uv *= 2.0;
    uv -= 0.5;
    uv.x *= iResolution.x / iResolution.y;
    
    float r = 12.0*iMouse.x/iResolution.x;
    
    vec3 ro = vec3(8.0*sin(0.5*r),1.5-iMouse.y/iResolution.y,8.*cos(0.5*r));
    vec3 lookAt = vec3(0.0);
    vec3 cameraDir = normalize(lookAt-ro);
    vec3 up = vec3(0.0,1.0,0.0);
    vec3 left = normalize(cross(cameraDir, up)); // Might be right
    vec3 rd = cameraDir;
    float FOV = 0.4; // Not actual FOV, just a multiplier
    rd += FOV * up * uv.y;
    rd += FOV * left * uv.x;
    // `rd` is now a point on the film plane, so turn it back to a direction
    rd = normalize(rd);
    
    vec2 t;
    vec3 pos;
    float size;
    int iter;
    bool hit = trace(ro, rd, t, pos, iter, size);
    vec3 col =  hit ? vec3((t.x-6.)*.5,0.,0.)+testColor : vec3(.0,0.,0.);

    //fragColor = vec4(vec3(float(iter)/float(MAX_ITER)),1.);
    fragColor = vec4(col,1.0);
}