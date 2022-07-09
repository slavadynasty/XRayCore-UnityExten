// govno.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include <iostream>
#include "xr_ogf.h"
using namespace xray_re;

struct xinfl_pred {
    bool operator()(const fbone_weight& l, const fbone_weight& r) {
        return l.weight > r.weight;
    }
};

int main()
{
    xr_ogf* ogf = xr_ogf::load_ogf("C:\\Users\\Slava\\Desktop\\stalker_neutral_1.ogf");
    ogf->bones().at(0)->calculate_bind(fmatrix().identity());
    int* bone_refs = 0;
    bone_refs = new int[ogf->bones().size()];
    int ds = 0;
    ogf = ogf->children().at(0);
    size_t num_verts = ogf->vb().size();
    for (size_t i = 0; i < num_verts; i++)
    {
        if (ogf->vb().has_influences())
        {
            finfluence xinfl = ogf->vb().w(i);
            int num_xinfls = int(xinfl.size() & INT_MAX);

            if (num_xinfls == 1) {

            }
            else {
                xr_assert(num_xinfls > 0);
                std::sort(xinfl.begin(), xinfl.end(), xinfl_pred());
                int h;
                int weight;

                if (xinfl[3].bone != 0)
                {
                    std::cout << "Hello World!\n";
                }

                for (int j = 1; j != num_xinfls; ++j)
                {
                    h = bone_refs[xinfl[j].bone];
                }
                    

                for (int j = 0; j != num_xinfls && j != 3; ++j) {
                    weight = int(floorf(xinfl[j].weight * 100.f + 0.501f));
                }

                ds++;
            }
        }
    }

    std::cout << "Hello World!\n";
}

// Запуск программы: CTRL+F5 или меню "Отладка" > "Запуск без отладки"
// Отладка программы: F5 или меню "Отладка" > "Запустить отладку"

// Советы по началу работы 
//   1. В окне обозревателя решений можно добавлять файлы и управлять ими.
//   2. В окне Team Explorer можно подключиться к системе управления версиями.
//   3. В окне "Выходные данные" можно просматривать выходные данные сборки и другие сообщения.
//   4. В окне "Список ошибок" можно просматривать ошибки.
//   5. Последовательно выберите пункты меню "Проект" > "Добавить новый элемент", чтобы создать файлы кода, или "Проект" > "Добавить существующий элемент", чтобы добавить в проект существующие файлы кода.
//   6. Чтобы снова открыть этот проект позже, выберите пункты меню "Файл" > "Открыть" > "Проект" и выберите SLN-файл.
