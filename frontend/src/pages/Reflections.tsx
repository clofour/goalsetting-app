import { useGetApiReflectionList } from '@/api/endpoints/reflection/reflection';
import Bar from '@/components/reflections/Bar';
import Stats from '@/components/reflections/Stats';
import { SimpleGrid, Stack, Text } from '@mantine/core';

export default function Reflections() {
    const { data: response, error, isLoading, mutate } = useGetApiReflectionList();

    return (
        <Stack>
            <Stats />
            <SimpleGrid cols={3} h="100%">
                <Bar key="strength">
                    Hello!
                </Bar>
                <Bar key="weakness">

                </Bar>
                <Bar key="improvement">

                </Bar>
            </SimpleGrid>
        </Stack>
    );
}