# 关键字

HRESULT

# 第四章

## 杂项

纹理格式（数据类型）p79

深度缓冲数据格式 P83

功能级别P88

显示适配器的获取 P89

## COM

COM是组件对象模型（Component Object Model），有一个全局唯一标识ID（GUID），接口以I开头，ComPtr存在于Windows运行库wrl.h中，相当于COM对象的智能指针，使用完后需要使用release释放函数见龙书P78

## 交换链

DXGI_SWAP_CHAIN_DESC 结构是用来描述交换链（swap chain）的属性，这一结构在创建交换链时被用来指定交换链的各种配置。以下是 DXGI_SWAP_CHAIN_DESC 结构的详细定义：

```C
typedef struct DXGI_SWAP_CHAIN_DESC {
    DXGI_MODE_DESC BufferDesc;             // 描述前/后缓冲区的属性
    DXGI_SAMPLE_DESC SampleDesc;           // 多重采样的描述
    DXGI_USAGE BufferUsage;                // 表示缓冲区的用途
    UINT BufferCount;                      // 缓冲区数量
    HWND OutputWindow;                     // 输出窗口的句柄
    BOOL Windowed;                         // 指示窗口是窗口模式还是全屏模式
    DXGI_SWAP_EFFECT SwapEffect;           // 交换链的交换效果
    UINT Flags;                            // 一组选项标志
} DXGI_SWAP_CHAIN_DESC;
```

各字段的含义如下：

* BufferDesc：类型为 DXGI_MODE_DESC，描述了交换链中使用的缓冲区（通常指的是后缓冲区）的分辨率、格式等属性。
* SampleDesc：类型为 DXGI_SAMPLE_DESC，用来定义多重采样的配置，包括采样数量和质量级别。这对抗锯齿等图形优化特别重要。
* BufferUsage：这是一个枚举值，定义了缓冲区的用途，例如 DXGI_USAGE_RENDER_TARGET_OUTPUT 表示缓冲区将被用作渲染目标。
* BufferCount：指定交换链中的缓冲区数量。通常至少为2，这样可以启用双缓冲，从而减少撕裂和提高性能。
* OutputWindow：这是一个 HWND（窗口句柄），指定了交换链的输出目标窗口。
* Windowed：一个布尔值，指示交换链是在窗口模式下运行还是全屏模式下运行。
* SwapEffect：类型为 DXGI_SWAP_EFFECT，定义了交换链在呈现图像时的行为，如 DXGI_SWAP_EFFECT_DISCARD、DXGI_SWAP_EFFECT_FLIP_DISCARD 等。不同的交换效果会影响性能和资源的使用方式。
* Flags：这是一组标志，用于进一步定义交换链的行为，例如 DXGI_SWAP_CHAIN_FLAG_ALLOW_MODE_SWITCH 允许交换链在窗口模式和全屏模式之间自动切换。

在使用 DirectX 创建交换链时，需要正确设置这些参数以确保交换链的行为符合应用程序的需求。错误的配置可能导致性能问题或者图形输出不正确。如需进一步帮助，请随时提问。

## 资源和描述符（descriptor）

资源在GPU中只是一些存储在显存中的块，要告知GPU该如何处理这些资源，比如这些资源绑定到渲染管线的哪个部分，需要设置描述符，描述符也被称为视图（View）

常用的描述符：

* CBV/SRV/UAV描述符分别表示常量缓冲区视图，着色器资源视图和无序访问视图
* 采样器（sampler）描述符表示的是采样器资源（用于纹理）
* RTV表示渲染目标视图资源（render target view）
* DSV描述符表示深度/模板视图资源（depth/stencil view）

描述符堆（desciptor heap）中存储了一系列描述符（可以看作描述符数组）。

每个渲染管线的阶段，资源都要设置独立的资源描述符。似乎可以把描述符和OpenGL的uniform联系起来。

资源描述符最好在初始化时创建

## 超级采样（SSAA）和多重采样（MSAA）P87

SSAA会在更高的分辨率下进行光照计算，之后求一组像素的平均得到最终屏幕上的一个像素的颜色

MSAA是在原分辨率下进行光照计算，之后在更大分辨率的深度缓冲中计算每个子像素的深度，同时计算每个子像素是否被片元覆盖，深度缓冲用于判断是否使用近处子像素的颜色

## 功能级别

功能级别会从高到低依次检测，龙书中假设支持功能级别D3D_FEATURE_LEVEL_11_0

## DirectX图形基础结构

DirectX图形基础结构（DirectX Graphics Infrastructure，DXGI）

## 命令队列和命令列表

命令队列是环形缓冲区，每个GPU至少维护一个，CPU利用命令列表提交命令到这个队列当中，提交时，命令不会被GPU立即执行。命令队列由ID3D12CommandQueue表示，是一个COM对象，使用D3D12_COMMAND_QUEUE_DESC结构体描述，然后使用ID3D12Device::CreateCommandQueue来创建队列，要上传CPU的命令列表需要使用ExecuteCommandLists，在其中填入命令数量和命令列表的指针。

向命令列表添加命令，要使用ID3D12GraphicsCommandList接口的成员函数来实现，比如设置视口需要使用mCommandList->RSSetViewports(1,&mScreenViewport);，mCommandList为一个指向ID3D12GraphicsCommandList接口的指针。所以命令都被添加到命令列表后，必须使用指向命令列表接口的指针调用Close()来结束记录命令，也就是mCommandList->Close()，提交命令列表前一定要这么做。

命令列表是存储在命令分配器（command allocator）上的，命令分配器通过ID3D12Device来创建，命令列表也是通过它创建

创建命令分配器时需要指定，类型、COM ID和指向命令分配器的指针

命令列表的创建要指定GPU掩码、命令列表类型、关联的命令分配器、命令列表的渲染流水线初始状态（第六章介绍）、COM ID、指向命令列表的指针

一个命令分配器可以关联多个命令列表，但它关联的命令列表不能同时打开，当创建或重置命令列表时会让命令列表处于打开的状态，所以连续创建两个命令列表会报错，打开一个命令列表时，需要关闭其他命令列表

命令列表可以通过ID3D12GraphicsCommandList::Reset(pAllocator,pInitalState)来复用命令分配器中的命令列表占用的底层内存（不能理解为什么龙书P98说的内容，重置命令列表为什么不会影响命令队列中的命令，为什么命令分配器仍然在维护命令队列引用的系列命令，假设我当前正要写入命令列表的命令，写入的位置的原始命令还没执行呢？）

## CPU和GPU间的同步

当GPU还没有执行某个命令时，需要刷新命令队列，也就是创建围栏（fence）强制CPU等待，以避免CPU修改GPU需要的资源，围栏用ID3D12Fence接口来表示。

刷新命令队列可以通过mCommandQueue->Signal(mFence.Get(),mCurrentFence)来向命令队列末尾添加一个获取围栏值，设置新的围栏值的命令实现，在这之前需要初始化一个围栏，围栏来自ID3D12Device::CreateFence，需要一个初始值、flag、COM ID、指向围栏的指针，ID和指针可以使用IID_PPV_ARGS(&mFence)宏得到，初始值可以设置为0，然后围栏值设置为1，让CPU等待需要创建命中围栏的事件，获取事件句柄，并使用WaitForSingleObject(eventHandle, INFINITE)，让CPU等待，直到命中围栏，句柄使用后需要关闭，具体参考龙书P99

## 命令与多线程

命令队列是线程自由的，可以被多线程共享，命令分配器和命令列表不是线程自由的，只能在每个独立的线程内部使用，初始化程序时，必须指出命令列表的最大数量

![1742283178436](image/DX12Note/1742283178436.png)

![1742285863602](image/DX12Note/1742285863602.png)

![1742285828602](image/DX12Note/1742285828602.png)![1742285925972](image/DX12Note/1742285925972.png)

![1742285960144](image/DX12Note/1742285960144.png)

![1742366139032](image/DX12Note/1742366139032.png)

# 第六章

## 顶点与输入布局

DX中对顶点的描述和OpenGL相似，DX使用D3D12_INPUT_LAYOUT_DESC来描述顶点的结构，与OpenGL中的VAO有些相似

## 顶点缓冲区

顶点缓冲区是GPU中的实际资源，它存储在默认堆中，它的创建需要复制资源到上传堆，然后从上传堆再传入到默认堆中，这个默认堆也就是顶点缓冲区，DX12龙书中有一工具函数d3dUtil::CreateDefaultBuffer，可以用于创建使用默认堆的缓冲区

顶点缓冲区需要绑定到渲染管线中使用，这种资源需要创建一个顶点缓冲区视图（VBV，Vertex Buffer View）。与RTV不同，它不需要创建描述符堆，它由D3D12_VERTEX_BUFFER_VIEW结构体来表示，这个结构体包含一个缓冲区中的虚拟地址，可以通过ID3D12Resource::GetGPUVirtualAddress来获取，还包括一个顶点缓冲区大小和其中每个元素的大小，因此VBV和OpenGL的VBO很相似

要绑定渲染管线，需要使用ID3D12GraphicsCommandList::IASetVertexBuffers，这个函数需要放入起始输入槽位置（输入槽一共16个，索引从0开始），视图的数量和一个视图数组的指针

绑定顶点缓冲区后，通过ID3D12GraphicsCommandList::DrawInstanced绘制顶点，它需要放入每个实例的顶点数，实例数量，顶点缓冲区初始偏移量（从0开始），实例相关参数（目前设置为0）

![1742370887182](image/DX12Note/1742370887182.png)

另外还需要指定图元类型，通过以下命令设置：

```C++
cmdList->IASetPrimitiveTopology(D3D_PRIMITIVE_TOPOLOGY_TRIANGLELIST);
```

## 索引缓冲区

索引缓冲区D3D12_INDEX_BUFFER_VIEW和OpenGL中的EBO相似，利用顶点的索引，重复使用顶点数据，不用每个三角形都创建自己独立的顶点，比如对于一个四边形来说，需要创建两个三角形，那么就有6个顶点的数据，其中有两对顶点是相同的顶点数据，这时就产生了浪费，使用索引缓冲区，通过索引组成图元，那么就只需要4个顶点的数据和6个无符号整数索引数据，就能绘制出四边形了

索引的数据类型只能是16位的DXGI_FORMAT_R16_UINT或者是32位的DXGI_FORMAT_R16_UINT

使用索引缓冲区之前，需要创建默认堆，然后使用ID3D12GraphicsCommandList::IASetIndexBuffer将索引缓冲区绑定到输入装配器阶段，这个函数只需要输入IBV

绘制索引缓冲区的内容需要使用ID3D12GraphicsCommandList::DrawIndexInstanced，放入每个实例的索引总数，实例的数量，索引偏移量，该实例在顶点缓冲区的起始位置，实例相关参数（填0即可）

![1742372620677](image/DX12Note/1742372620677.png)

## 顶点着色器示例

不使用结构体管理输入输出

![1742374030987](image/DX12Note/1742374030987.png)

使用结构体管理输入输出

```C
cbuffer cbPerObject : register(b0)
{
    float4x4 gWorldViewProj; 
};

struct VertexIn
{
    float3 PosL  : POSITION;
    float4 Color : COLOR;
};

struct VertexOut
{
    float4 PosH  : SV_POSITION;
    float4 Color : COLOR;
};

VertexOut VS(VertexIn vin)
{
    VertexOut vout;

    // Transform to homogeneous clip space.
    vout.PosH = mul(float4(vin.PosL, 1.0f), gWorldViewProj);

    // Just pass vertex color into the pixel shader.
    vout.Color = vin.Color;
  
    return vout;
}

float4 PS(VertexOut pin) : SV_Target
{
    return pin.Color;
}
```

在上面的代码中cbuffer代表常量缓冲区，其后是该常量缓冲区的名字以及它存放的寄存器槽位（register(b0)），输入的值，比如上面代码中的POSITION和COLOR，必须要和D3D12_INPUT_ELEMENT_DESC中输入的字符串一致

注意，如果着色器中的类型与描述符的类型不一致，编译器只会发出警告，但这种操作是合法的

## 像素着色器

像素着色器中传入的数据要和前面的着色器传出的数据相关联

![1742374730045](image/DX12Note/1742374730045.png)

另外需要注意DX的着色器需要写到同一个hlsl文件中，和OpenGL不同着色器用不同文件有所区别

## 常量缓冲区

常量缓冲区中的数据读取是以256B为单位读取的，龙书提供了一个工具函数d3dUtil::CalcConstantBufferByteSize，将缓冲区大小凑整为硬件最小分配空间256B的整数倍

另外DX12推出了新的着色器模型(shader model, SM)5.1，它要求先建立一个结构体，然后使用一个特殊语法来生成常量缓冲区

```C++
struct ObjectConstants
{
    float4x4 gWVP;
    uint matIndex;
};
ConstantBuffer<ObjectConstants> gObjConstants : register(b0);
```

