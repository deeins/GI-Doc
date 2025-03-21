# Game类

```C++
class Game
{
public:
	static Game& getInstance()
               {
		static Game game;
		return game;
	}
	void init();
	void run();
	void handleEvents();
	void update();
	void render();
	void clean();
private:
	unsigned int screenWidth = 1280;
	unsigned int screenHeith = 720;
	Game() {}
	Game(const Game&) = delete;
	Game& operator=(const Game&) = delete;
	~Game() {}
};
```

# 3DTank

PC平台使用DX12进行开发

## Actor类

包含组件类成员、网格成员、变换相关属性

```C++
class Actor
{
	Transform trans;
	vector<Component*> comps;
};
```

## 资源管理

单例模式，使用字典存储资源，因此每个需要获取资源的对象需要一个编号来标识

```c++
class AssetManager
{
	unordered_map<const char*,const char*> sourceMap;
	const char* read(const char* path);
};
```

## 物理

物理需要用于静态对象和动态对象，需要执行的功能：

射线检测、碰撞检测

```c++
class Physic
{
	Mesh* collisionBox;//最好只包含顶点信息
	float mass;
	bool sweep(vec3 direction,Scene* scene);//球面扫描或射线检测
	void physicSimulate(Actor* actor);//根据质量控制下降
}
```

## 渲染

网格类、模型类、场景类、变换类、shader管理类、摄像机类、材质类

```C++
struct Vertex
{
	pos,normal,texcoord;
};
class Mesh
{
	unsigned int indices[];
	vector<Texture2D> textures;
	Vertex vertex[];
	void draw();
};
class Model
{
	Mesh* mesh;

}
```

## 输入

考虑使用轴映射、事件绑定实现，指令缓冲，单独线程

## 组件

使用vector作为成员存储到Actor类中

## Timer

使用C++11的chrono库获取当前时间，用于实现一些帧活动

## 事件

需要实现委托，触发一个事件时，响应一个或多个事件

下面是一些UE常见的事件类型：

1. **输入事件** ：这些事件响应玩家的输入，如按键、鼠标点击或游戏手柄操作。在 UE 中，你可以设置特定的输入事件来响应键盘按键、鼠标移动和点击、触摸输入等。
2. **碰撞事件** ：当游戏中的对象相互碰撞时触发。例如，当玩家的角色与游戏世界中的某个物体发生碰撞时，可以触发一个事件来处理碰撞的结果。
3. **定时器事件** ：这些事件在设定的时间间隔后触发，可以用于创建重复发生的效果，比如定时更新游戏状态或触发周期性的游戏行为。
4. **动画事件** ：在动画序列中的特定时间点触发，常用于同步动画和游戏逻辑，例如，在动画的某个特定点触发伤害计算或播放声音。
5. **生命周期事件** ：这些事件与游戏对象的生命周期相关，如对象的创建、启动、每帧更新、销毁等。在 UE 的蓝图中，这些通常是如 `BeginPlay` 和 `EndPlay`。
6. **自定义事件** ：在蓝图中，你可以定义自己的事件，并在需要时手动触发这些事件。这使得事件驱动的逻辑变得非常灵活。
7. **网络事件** ：用于多玩家游戏，处理如复制和网络状态变更的事件。
8. **触发器事件** ：当角色或对象进入或离开预设区域（通常是触发器体积）时触发。
9. **委托和多播事件** ：在UE中，委托是一种允许将多个方法绑定到单一事件的机制，而多播委托允许事件被多个订阅者监听。

## AI

状态机

## Particle

使用几何着色器实现，烟雾、炸弹特效

## UI

支持多语言扩展

### 字体

## 声音

3D空间感，不限插件

## 数学库

因为要跨平台，所以需要自己实现
