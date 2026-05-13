import { useGetApiReflectionList } from '@/api/endpoints/reflection/reflection';
import Bar from '@/components/reflections/Bar';
import { SimpleGrid, Text } from '@mantine/core';

export default function Reflections() {
    const { data: response, error, isLoading, mutate } = useGetApiReflectionList();

    return (
        <>
            <SimpleGrid cols={3}>
                <Bar>
                    
                </Bar>
                <Bar>
                    
                </Bar>
                <Bar>
                    
                </Bar>
            </SimpleGrid>
        </>
    );
}