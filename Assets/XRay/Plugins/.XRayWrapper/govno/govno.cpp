// govno.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include <iostream>
#include "xr_ogf.h"
#include "xr_ogf_v4.h"
using namespace xray_re;

int main()
{
    xr_ogf* ogf = xr_ogf::load_ogf("C:\\Users\\Slava\\Desktop\\stalker_neutral_1.ogf");
    ogf->bones().at(0)->calculate_bind(fmatrix().identity());
    ogf = ogf->children().at(0);
    
    xr_ogf_v4* ogf4 = new xr_ogf_v4;
    ogf4->load_omf("D:\\Games\\S.T.A.L.K.E.R. COP\\gamedataUE\\meshes\\actors\\critical_hit_grup_1.omf");

    xr_skl_motion_vec skls = ogf4->motions();
    for (size_t i = 0; i < skls.size(); i++)
    {
        xr_skl_motion* skl = skls.at(i);

        xr_bone_motion_vec bmots = skl->bone_motions();
        for (size_t j = 0; j < bmots.size(); j++)
        {
            xr_bone_motion* bmot = bmots.at(j);
            bmot->
            std::cout << "Hello World!\n";
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
