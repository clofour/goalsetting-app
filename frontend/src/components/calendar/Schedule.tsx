import { Schedule } from '@mantine/schedule';
import { useGetApiEventGet } from '@/api/endpoints/event/event';

export default function Calendar() {
    const { data: response, error, isLoading, mutate } = useGetApiEventGet();

    return (
        <>
            <Schedule
                events={response?.data}
                layout="responsive"
            />
        </>
    );
}

